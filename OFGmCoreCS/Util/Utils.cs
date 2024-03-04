using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;

namespace OFGmCoreCS.Util
{
    public static class Utils
    {
        internal const string OFGmCoreCS = "\r\n ██████╗ ███████╗ ██████╗ ███╗   ███╗     ██████╗ ██████╗ ██████╗ ███████╗     ██████╗ ██╗ ██╗ " +
                                           "\r\n██╔═══██╗██╔════╝██╔════╝ ████╗ ████║    ██╔════╝██╔═══██╗██╔══██╗██╔════╝    ██╔════╝████████╗" +
                                           "\r\n██║   ██║█████╗  ██║  ███╗██╔████╔██║    ██║     ██║   ██║██████╔╝█████╗      ██║     ╚██╔═██╔╝" +
                                           "\r\n██║   ██║██╔══╝  ██║   ██║██║╚██╔╝██║    ██║     ██║   ██║██╔══██╗██╔══╝      ██║     ████████╗" +
                                           "\r\n╚██████╔╝██║     ╚██████╔╝██║ ╚═╝ ██║    ╚██████╗╚██████╔╝██║  ██║███████╗    ╚██████╗╚██╔═██╔╝" +
                                           "\r\n ╚═════╝ ╚═╝      ╚═════╝ ╚═╝     ╚═╝     ╚═════╝ ╚═════╝ ╚═╝  ╚═╝╚══════╝     ╚═════╝ ╚═╝ ╚═╝ " ;

        public static string LineSeparator = Environment.NewLine + Environment.NewLine;

        public static NumberFormatInfo simpleProvider = new NumberFormatInfo
        {
            NumberDecimalSeparator = ".",
            NumberGroupSeparator = ","
        };

        public static string GetHardwareInfo(string win32Class, string classItemField)
        {
            List<string> result = new List<string>();
            
            foreach (ManagementObject obj in new ManagementObjectSearcher("SELECT * FROM " + win32Class).Get().Cast<ManagementObject>())
            {
                result.Add(obj[classItemField].ToString().Trim());
            }
            
            return string.Join(", ", result);
        }
    }
}
