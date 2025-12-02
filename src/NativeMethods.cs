using System.Runtime.InteropServices;

namespace WinKit
{
    public static class NativeMethods
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out Point point);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint cInputs, out Input pInputs, int cbSize);

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
