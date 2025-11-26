using Microsoft.Extensions.Logging;
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
                    AutoMouseMoverIntervalSeconds = Math.Max((int)numAutoMouseMoverInterval.Value, 5),
                    AutoMouseMoverPixel = Math.Max((int)numAutoMouseMoverPixel.Value, 5),
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
        }

        private async void OnMainFormShown(object sender, EventArgs e)
        {
            try
            {
                var userSetting = await _userSettingStore.GetUserSettingAsync();
                SyncSettingToUI(userSetting);
            }
            catch (Exception ex)
            {
                await _logger.LogAsync("Failed to load user settings.", ex);
            }
        }
    }
}
