using System.Runtime.InteropServices;

namespace WinKit
{
    internal sealed class MessageWindow : NativeWindow, IDisposable
    {
        private readonly Action _onShowMeMessage;

        public MessageWindow(uint showMeMessage, Action onShowMeMessage)
        {
            _onShowMeMessage = onShowMeMessage;
            
            var cp = new CreateParams
            {
                Style = 0,
                ExStyle = 0,
                ClassStyle = 0,
                Caption = "WinKit Message Window",
                Parent = IntPtr.Zero
            };

            CreateHandle(cp);
            
            ShowMeMessage = showMeMessage;
        }

        public uint ShowMeMessage { get; }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == ShowMeMessage && ShowMeMessage != 0)
            {
                _onShowMeMessage?.Invoke();
            }

            base.WndProc(ref m);
        }

        public void Dispose()
        {
            DestroyHandle();
        }
    }
}
