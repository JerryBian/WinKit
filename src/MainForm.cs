using WinKit.Extensions;
using WinKit.Logging;
using WinKit.Models;
using WinKit.Properties;
using WinKit.Store;

namespace WinKit
{
    public partial class MainForm : Form
    {
        private readonly IFileLogger _logger;
        private readonly IUserSettingStore _userSettingStore;
        private readonly string _appUserModelId;
        private bool _isExiting = false;
        private MessageWindow _messageWindow;

        public MainForm(IFileLogger logger, IUserSettingStore userSettingStore, string appUserModelId)
        {
            _logger = logger;
            _userSettingStore = userSettingStore;
            _appUserModelId = appUserModelId;

            Icon = Resources.Icon;
            InitializeComponent();
        }

        public void SetShowMeMessage(uint message)
        {
            _messageWindow?.Dispose();
            _messageWindow = new MessageWindow(message, ShowAndActivate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _messageWindow?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            NativeMethods.SetWindowAppUserModelId(Handle, _appUserModelId);
        }

        private void ShowAndActivate()
        {
            if (InvokeRequired)
            {
                Invoke(ShowAndActivate);
                return;
            }

            if (!Visible)
            {
                Show();
            }
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            ShowInTaskbar = true;
            NativeMethods.ShowWindowToFront(Handle);
            Activate();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ShowInTaskbar = true;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_NOCLOSE = 0x200;
                var cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CS_NOCLOSE;
                return cp;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_isExiting)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
            }
            base.OnFormClosing(e);
        }

        private void OnMainFormResize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                ShowInTaskbar = false;
            }
        }

        private void OnNotifyIconDoubleClick(object sender, EventArgs e)
        {
            ShowAndActivate();
        }

        private void OnExitMenuItemClick(object sender, EventArgs e)
        {
            _isExiting = true;
            Application.Exit();
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
                    AutoShutdownPCAfterMinutes = Math.Max((int)numAutoShutdownAfter.Value, 1)
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

            labelAutoShutdownStatus.Text = remaining > TimeSpan.Zero
                ? $"PC will shutdown in {remaining:hh\\:mm\\:ss}."
                : "Shutdown imminent...";
        }
    }
}
