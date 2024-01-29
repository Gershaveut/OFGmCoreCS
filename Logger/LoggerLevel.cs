using System.Drawing;
using static OFGmCoreCS.Logger.Logger;

namespace OFGmCoreCS.Logger
{
    public enum LoggerLevel
    {
        Info,
        Warn,
        Error,
        Debug
    }

    public class LoggerLevelColor
    {
        public Color GetColor(LoggerLevel level)
        {
            switch (level)
            {
                default: return Color.Black;
                case LoggerLevel.Info: return Color.Black;
                case LoggerLevel.Warn: return Color.FromArgb(164, 255, 164, 1); //Gold
                case LoggerLevel.Error: return Color.Red;
                case LoggerLevel.Debug: return Color.Gray;
            }
        }
    }
}
