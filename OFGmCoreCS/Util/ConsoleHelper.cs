using System.Runtime.InteropServices;

namespace OFGmCoreCS.Util
{
    public static class ConsoleHelper
    {
        public delegate void SignalHandler(ConsoleSignal consoleSignal);

        [DllImport("Kernel32", EntryPoint = "SetConsoleCtrlHandler")]
        public static extern bool SetSignalHandler(SignalHandler signalHandler, bool add);

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
