using WinKit.Extensions;
using WinKit.Logging;
using WinKit.Models;
using WinKit.Store;

namespace WinKit
{
    public partial class MainForm : Form
    {
        private readonly IFileLogger _logger;
        private readonly IUserSettingStore _userSettingStore;

        public MainForm(IFileLogger logger, IUserSettingStore userSettingStore)
        {
            _logger = logger;
            _userSettingStore = userSettingStore;

            InitializeComponent();
        }

        private async void OnBtnSaveClick(object sender, EventArgs e)
        {
            await ExecuteAsync(async () =>
            {
                var userSetting = new UserSetting
                {
                    AutoMouseMoverEnabled = cbAutoMouseMoverEnabled.Checked,
                    AutoMouseMoverClickEnabled = cbAutoMouseMoverClickEnabled.Checked,
                    AutoMouseMoverIntervalSeconds = Math.Max((int)numAutoMouseMoverInterval.Value, 5),
                    AutoMouseMoverPixel = Math.Max((int)numAutoMouseMoverPixel.Value, 5),
                    AutoMouseMoverDisableAfterInMinutes = (int)numAutoMouseMoverDisableAfter.Value,
                    AutoShutdownPCEnabled = cbAutoShutdownEnabled.Checked,
                    AutoShutdownPCAfterMinutes = (int)numAutoShutdownAfter.Value
                };

                await _userSettingStore.SaveAsync(userSetting);
                SyncSettingToUI(userSetting);
                await _logger.LogAsync($"User settings saved. {userSetting}");
                MessageBox.Show("Settings saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        private async Task ExecuteAsync(Func<Task> task)
        {
            try
            {
                await task.Invoke().OkForCancelAsync();
            }
            catch (Exception ex)
            {
                await _logger.LogAsync("An error occurred during execution.", ex);
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SyncSettingToUI(UserSetting userSetting)
        {
            cbAutoMouseMoverEnabled.Checked = userSetting.AutoMouseMoverEnabled;
            numAutoMouseMoverInterval.Value = userSetting.AutoMouseMoverIntervalSeconds;
            numAutoMouseMoverPixel.Value = userSetting.AutoMouseMoverPixel;
            numAutoMouseMoverDisableAfter.Value = userSetting.AutoMouseMoverDisableAfterInMinutes;
            numAutoShutdownAfter.Value = userSetting.AutoShutdownPCAfterMinutes;
            cbAutoMouseMoverClickEnabled.Checked = userSetting.AutoMouseMoverClickEnabled;
            cbAutoShutdownEnabled.Checked = userSetting.AutoShutdownPCEnabled;
        }

        private async void OnMainFormShown(object sender, EventArgs e)
        {
            try
            {
                var userSetting = await _userSettingStore.GetUserSettingAsync();
                if (userSetting.AutoShutdownPCEnabled)
                {
                    userSetting.AutoShutdownPCEnabled = false;
                    await _userSettingStore.SaveAsync(userSetting);
                }

                SyncSettingToUI(userSetting);
            }
            catch (Exception ex)
            {
                await _logger.LogAsync("Failed to load user settings.", ex);
            }
        }

        public async Task UpdateAutoMouseStatusAsync(bool enabled, long moveCount, TimeSpan remaining)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                await InvokeAsync(async ct => await UpdateAutoMouseStatusAsync(enabled, moveCount, remaining), CancellationToken.None);  
                return;
            }

            if(!enabled)
            {
                labelRemainingTime.Text = "Mouse mover is disabled.";
                labelMoveCount.Text = string.Empty;
                return;
            }

            // Update controls directly on UI thread
            labelMoveCount.Text = $"Mouse moved {moveCount} times.";
            labelRemainingTime.Text = remaining > TimeSpan.Zero
                ? $"Mouse mover will be disabled in {remaining.TotalMinutes:F1} minutes."
                : "Mouse mover is active.";
        }

        public async Task UpdateAutoShutdownPCAsync(bool enabled, TimeSpan remaining)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                await InvokeAsync(async ct => await UpdateAutoShutdownPCAsync(enabled, remaining), CancellationToken.None);
                return;
            }

            if (!enabled)
            {
                labelAutoShutdownStatus.Text = "Auto shutdown is disabled.";
                return;
            }

            labelAutoShutdownStatus.Text = $"PC will shutdown in {remaining:hh\\:mm\\:ss}.";
        }
    }
}
