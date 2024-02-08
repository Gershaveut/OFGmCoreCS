using System;

namespace OFGmCoreCS.Util
{
    public static class CrashReporter
    {
        public static string CreateReport(Exception exception)
        {
            string report = "";
            
            report += $"Crash Report {DateTime.Now}";

            report += Utils.NewLine(exception.ToString());

            report += Utils.NewLine(exception.TargetSite.ToString());

            report += Utils.NewLine(exception.HelpLink);

            return report;
        }

        public static string CreateReport(Exception exception, FileLogger fileLogger)
        {
            string report = CreateReport(exception);

            fileLogger.SaveFile(report, false);

            return report;
        }
    }
}
