using System.Runtime.InteropServices;

namespace WinKit
{
    public static class NativeMethods
    {
        private const int SW_RESTORE = 9;
        private const int SW_SHOW = 5;
        private const int HWND_BROADCAST = 0xFFFF;

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out Point point);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint cInputs, out Input pInputs, int cbSize);

        [DllImport("shell32.dll", SetLastError = true)]
        private static extern int SetCurrentProcessExplicitAppUserModelID([MarshalAs(UnmanagedType.LPWStr)] string AppID);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern uint RegisterWindowMessage(string lpString);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public static bool GetCursorPosition(out Point point)
        {
            return GetCursorPos(out point);
        }

        public static bool SetCursorPosition(int x, int y)
        {
            return SetCursorPos(x, y);
        }

        public static void SendMouseClickEvent()
        {
            var inputDown = new Input();
            inputDown.Type = 0; // INPUT_MOUSE
            inputDown.Data.Mouse.MouseData = 0;
            inputDown.Data.Mouse.Time = 0;
            inputDown.Data.Mouse.X = 0;
            inputDown.Data.Mouse.Y = 0;
            inputDown.Data.Mouse.Flags = 0x0002; // MOUSEEVENTF_LEFTDOWN
            inputDown.Data.Mouse.ExtraInfo = IntPtr.Zero;

            var inputUp = new Input();
            inputUp.Type = 0; // INPUT_MOUSE
            inputUp.Data.Mouse.MouseData = 0;
            inputUp.Data.Mouse.Time = 0;
            inputUp.Data.Mouse.X = 0;
            inputUp.Data.Mouse.Y = 0;
            inputUp.Data.Mouse.Flags = 0x0004; // MOUSEEVENTF_LEFTUP
            inputUp.Data.Mouse.ExtraInfo = IntPtr.Zero;

            // Send down and up separately
            SendInput(1, out inputDown, Marshal.SizeOf(new Input()));
            SendInput(1, out inputUp, Marshal.SizeOf(new Input()));
        }

        public static void SetAppUserModelId(string appId)
        {
            SetCurrentProcessExplicitAppUserModelID(appId);
        }

        public static uint GetShowMeMessage(string uniqueIdentifier)
        {
            return RegisterWindowMessage($"WinKit_ShowMe_{uniqueIdentifier}");
        }

        public static void BroadcastShowMeMessage(string uniqueIdentifier)
        {
            uint message = GetShowMeMessage(uniqueIdentifier);
            PostMessage((IntPtr)HWND_BROADCAST, message, IntPtr.Zero, IntPtr.Zero);
        }

        public static void ShowWindowToFront(IntPtr hWnd)
        {
            if (hWnd != IntPtr.Zero)
            {
                if (IsIconic(hWnd))
                {
                    ShowWindow(hWnd, SW_RESTORE);
                }
                else
                {
                    ShowWindow(hWnd, SW_SHOW);
                }
                SetForegroundWindow(hWnd);
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Input
    {
        public uint Type;
        public InputUnion Data;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct InputUnion
    {
        [FieldOffset(0)]
        public MouseInput Mouse;
        [FieldOffset(0)]
        public KeyboardInput Keyboard;
        [FieldOffset(0)]
        public HardwareInput Hardware;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MouseInput
    {
        public int X;
        public int Y;
        public uint MouseData;
        public uint Flags;
        public uint Time;
        public IntPtr ExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardInput
    {
        public ushort VirtualKey;
        public ushort ScanCode;
        public uint Flags;
        public uint Time;
        public IntPtr ExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HardwareInput
    {
        public uint UMsg;
        public ushort wParamL;
        public ushort wParamH;
    }
}
