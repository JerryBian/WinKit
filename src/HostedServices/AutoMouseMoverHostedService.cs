using Microsoft.Extensions.Hosting;
using WinKit.Extensions;
using WinKit.Logging;
using WinKit.Models;
using WinKit.Store;

namespace WinKit.HostedServices
{
    public class AutoMouseMoverHostedService(IFileLogger logger, IUserSettingStore userSettingStore, MainForm mainForm)
        : BackgroundService
    {
        private readonly MainForm _mainForm = mainForm;
        private readonly IFileLogger _logger = logger;
        private readonly IUserSettingStore _userSettingStore = userSettingStore;

        private Point _lastCursorPosition;
        private bool _moveBack;
        private DateTime _lastMovedAt;
        private long _moveCount;

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _lastMovedAt = DateTime.Now;
            _moveCount = 0L;
            await base.StartAsync(cancellationToken);
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
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
                        if (userSetting.AutoMouseMoverDisableAfterInMinutes <= 0 || (DateTime.Now - _lastMovedAt) < TimeSpan.FromMinutes(userSetting.AutoMouseMoverDisableAfterInMinutes))
                        {
                            await MoveMouseAsync(userSetting);
                            _moveCount++;
                            _lastMovedAt = DateTime.Now;
                        }
                    }
                    else
                    {
                        _moveCount = 0;
                        _lastMovedAt = DateTime.Now;
                    }

                    var remainingTime = userSetting.AutoMouseMoverDisableAfterInMinutes > 0
                        ? TimeSpan.FromMinutes(userSetting.AutoMouseMoverDisableAfterInMinutes) - (DateTime.Now - _lastMovedAt)
                        : TimeSpan.Zero;
                    await _mainForm.UpdateAutoMouseStatusAsync(userSetting.AutoMouseMoverEnabled, _moveCount, remainingTime);

                    await Task.Delay(TimeSpan.FromSeconds(userSetting.AutoMouseMoverIntervalSeconds), stoppingToken).OkForCancelAsync();
                }
                catch (Exception ex)
                {
                    await _logger.LogAsync($"AutoMouseMoverHostedService failed.", ex);
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken).OkForCancelAsync();
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
                if (userSetting.AutoMouseMoverClickEnabled)
                {
                    NativeMethods.SendMouseClickEvent();
                }

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
