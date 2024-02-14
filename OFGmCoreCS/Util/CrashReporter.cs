using System;
using System.Text;

namespace OFGmCoreCS.Util
{
    public static class CrashReporter
    {
        public static string CreateReport(Exception exception)
        {
            return CreateReport(exception, null, null);
        }

        public static string CreateReport(Exception exception, FileLogger fileLogger)
        {
            return CreateReport(exception, fileLogger, null);
        }

        public static string CreateReport(Exception exception, FileLogger fileLogger, string additionally)
        {
            StringBuilder report = new StringBuilder();

            report.AppendLine($"/// {exception.Source} Crash Report {DateTime.Now} ///" + Environment.NewLine);
            report.AppendLine(exception.ToString() + Environment.NewLine);
            report.AppendLine("StackTrace:" + Environment.NewLine + exception.StackTrace + Environment.NewLine);

            report.AppendLine($"/// System Details ///" + Environment.NewLine);
            report.AppendLine("Operating System: " + Environment.OSVersion);
            report.AppendLine("Processor: " + Utils.GetHardwareInfo("Win32_Processor", "Name"));
            report.AppendLine($"Graphics Device: {Utils.GetHardwareInfo("Win32_VideoController", "Name")} {Utils.GetHardwareInfo("Win32_VideoController", "AdapterCompatibility")} {Utils.GetHardwareInfo("Win32_VideoController", "DriverVersion")}");

            string[] physicalMemory = Utils.GetHardwareInfo("Win32_PhysicalMemory", "Capacity").Split(',');
            long memory = 0;
            foreach (string memorys in physicalMemory)
                memory += Convert.ToInt64(memorys);
            report.AppendLine($"Memory: {memory} bytes ({memory / (1024 * 1024)} Mb)");

            if (!string.IsNullOrEmpty(additionally))
            {
                report.AppendLine(Environment.NewLine + $"/// Additionally ///");
                report.AppendLine(Environment.NewLine + additionally);
            }

            fileLogger?.SaveFile(report.ToString(), false);

            return report.ToString();
        }
    }
}
