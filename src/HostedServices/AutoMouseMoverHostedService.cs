using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using WinKit.Extensions;
using WinKit.Logging;
using WinKit.Models;
using WinKit.Store;

namespace WinKit.HostedServices
{
    public class AutoMouseMoverHostedService(IFileLogger logger, IUserSettingStore userSettingStore) : BackgroundService
    {
        private readonly IFileLogger _logger = logger;
        private readonly IUserSettingStore _userSettingStore = userSettingStore;

        private Point _lastCursorPosition;
        private bool _moveBack;

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _logger.LogAsync("AutoMouseMoverHostedService is starting.");
            if (!Environment.UserInteractive)
            {
                await _logger.LogAsync("AutoMouseMover is not working due to the process is not run under user session");
                return;
            }

            NativeMethods.GetCursorPosition(out _lastCursorPosition);
            _moveBack = false;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var userSetting = await _userSettingStore.GetUserSettingAsync();
                    if (userSetting.AutoMouseMoverEnabled)
                    {
                        await MoveMouseAsync(userSetting);
                    }

                    await Task.Delay(TimeSpan.FromSeconds(userSetting.AutoMouseMoverIntervalSeconds), stoppingToken).OkForCancelAsync();
                }
                catch (Exception ex)
                {
                    await _logger.LogAsync($"AutoMouseMoverHostedService failed.", ex);
                }
            }
        }

        private async Task MoveMouseAsync(UserSetting userSetting)
        {
            if (NativeMethods.GetCursorPosition(out var currentPosition))
            {
                if (currentPosition != _lastCursorPosition)
                {
                    await _logger.LogAsync($"User moved mouse to {currentPosition}, lastCusorPosition {_lastCursorPosition}, skip automatic move.");
                    _lastCursorPosition = currentPosition;
                    return;
                }

                var movePixel = _moveBack ? -1 * userSetting.AutoMouseMoverPixel : userSetting.AutoMouseMoverPixel;
                var newPosition = await FindNewPositionAsync(currentPosition, movePixel);
                NativeMethods.SetCursorPosition(newPosition.X, newPosition.Y);
                NativeMethods.SendMouseClickEvent();

                await _logger.LogAsync($"Moved mouse from {currentPosition} to {newPosition}.");
                _moveBack = !_moveBack;
                _lastCursorPosition = newPosition;
            }
            else
            {
                await _logger.LogAsync("GetCursorPosition failed.");
            }
        }

        private async Task<Point> FindNewPositionAsync(Point currentPosition, int movePixel)
        {
            var currentScreen = Screen.AllScreens.FirstOrDefault(s => s.Bounds.Contains(currentPosition));
            if (currentScreen == null)
            {
                return currentPosition;
            }

            var newX = currentPosition.X + movePixel;
            var newY = currentPosition.Y + movePixel;
            var newPosition = new Point(newX, newY);
            var bounds = currentScreen.Bounds;

            // Check if new position is within current screen bounds
            if (bounds.Contains(newPosition))
            {
                return newPosition;
            }

            var centerX = bounds.Left + bounds.Width / 2;
            var centerY = bounds.Top + bounds.Height / 2;
            await _logger.LogAsync($"Fallback to screen {currentScreen.DeviceName} center. Current postion {currentPosition}, movePixel {movePixel}.");
            return new Point(centerX, centerY);
        }
    }
}
