using System;
using System.Runtime.InteropServices;

namespace OFGmCoreCS.Util
{
    public static class ConsoleHelper
    {
        public delegate void SignalHandler(ConsoleSignal consoleSignal);

        [DllImport("Kernel32", EntryPoint = "SetConsoleCtrlHandler")]
        private static extern bool SetSignalHandler(SignalHandler signalHandler, bool add);

        public static bool SetSignal(SignalHandler signalHandler, bool add)
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                return SetSignalHandler(signalHandler, add);

            return false;
        }

        public enum ConsoleSignal
        {
            CtrlC = 0,
            CtrlBreak = 1,
            Close = 2,
            LogOff = 5,
            Shutdown = 6
        }
    }
}
