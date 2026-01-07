using ExecDotnet;
using Microsoft.Extensions.Hosting;
using WinKit.Extensions;
using WinKit.Logging;
using WinKit.Store;

namespace WinKit.HostedServices
{
    public class AutoShutdownPCHostedService(IFileLogger logger, IUserSettingStore userSettingStore, MainForm mainForm)
        : BackgroundService
    {
        private readonly MainForm _mainForm = mainForm;
        private readonly IFileLogger _logger = logger;
        private readonly IUserSettingStore _userSettingStore = userSettingStore;

        private DateTime _startedAt;

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _startedAt = DateTime.Now;
            await base.StartAsync(cancellationToken);
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var userSetting = await _userSettingStore.GetUserSettingAsync();
                    if (userSetting.LastUpdatedAt > _startedAt)
                    {
                        _startedAt = DateTime.Now;
                    }

                    if (userSetting.AutoShutdownPCEnabled && (DateTime.Now - _startedAt) > TimeSpan.FromMinutes(userSetting.AutoShutdownPCAfterMinutes))
                    {
                        await _logger.LogAsync("Shutdown threshold reached. Executing shutdown command.");
                        await Exec.RunAsync("shutdown /s /f /t 3", stoppingToken);
                        await _logger.LogAsync("Shutdown command executed.");
                        break;
                    }

                    var remainingTime = TimeSpan.FromMinutes(userSetting.AutoShutdownPCAfterMinutes) - (DateTime.Now - _startedAt);
                    await _mainForm.UpdateAutoShutdownPCAsync(userSetting.AutoShutdownPCEnabled, remainingTime);
                    await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken).OkForCancelAsync();
                }
                catch (Exception ex)
                {
                    await _logger.LogAsync($"AutoShutdownPCHostedService failed.", ex);
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken).OkForCancelAsync();
                }
            }
        }
    }
}
