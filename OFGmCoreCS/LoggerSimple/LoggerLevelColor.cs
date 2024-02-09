using System.Drawing;

namespace OFGmCoreCS.LoggerSimple
{
    public class LoggerLevelColor
    {
        public static Color info = Color.Black;
        public static Color warn = Color.Goldenrod;
        public static Color error = Color.Red;
        public static Color debug = Color.Gray;
        
        public static Color GetColor(LoggerLevel level)
        {
            switch (level)
            {
                default: return info;
                case LoggerLevel.Info: return info;
                case LoggerLevel.Warn: return warn;
                case LoggerLevel.Error: return error;
                case LoggerLevel.Debug: return debug;
            }
        }
    }
}
