using System;

namespace OFGmCoreCS.Util
{
    public static class CrashReporter
    {
        public static string CreateReport(Exception exception)
        {
            string report = "";

            report += $"/// {exception.Source} Crash Report {DateTime.Now} ///";

            report += Utils.LineSeparator + exception.ToString();
            report += Utils.LineSeparator + "StackTrace:" + Environment.NewLine + exception.StackTrace;

            report += Utils.LineSeparator + $"/// System Details ///";

            report += Utils.LineSeparator + "Operating System: " + Environment.OSVersion;
            report += Environment.NewLine + "Processor: " + Utils.GetHardwareInfo("Win32_Processor", "Name");
            report += Environment.NewLine + $"Graphics Device: {Utils.GetHardwareInfo("Win32_VideoController", "Name")} {Utils.GetHardwareInfo("Win32_VideoController", "AdapterCompatibility")} {Utils.GetHardwareInfo("Win32_VideoController", "DriverVersion")}";

            string[] physicalMemory = Utils.GetHardwareInfo("Win32_PhysicalMemory", "Capacity").Split(',');
            long memory = Convert.ToInt64(physicalMemory[0]) + Convert.ToInt64(physicalMemory[1]);
            report += Environment.NewLine + "Memory: " + memory + $" bytes ({memory / (1024 * 1024)} Mb)";

            return report;
        }

        public static string CreateReport(Exception exception, FileLogger fileLogger)
        {
            string report = CreateReport(exception);

            fileLogger.SaveFile(report, false);

            return report;
        }

        public static string CreateReport(Exception exception, FileLogger fileLogger, string additionally)
        {
            return CreateAdditionally(CreateReport(exception, fileLogger), additionally);
        }

        public static string CreateReport(Exception exception, string additionally)
        {
            return CreateAdditionally(CreateReport(exception), additionally);
        }

        private static string CreateAdditionally(string report, string additionally)
        {
            report += Utils.LineSeparator + $"/// Additionally ///";
            report += Utils.LineSeparator + additionally;

            return report;
        }
    }
}
