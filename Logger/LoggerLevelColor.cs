using System.Drawing;

namespace OFGmCoreCS.Logger
{
    public class LoggerLevelColor
    {
        public static Color GetColor(LoggerLevel level)
        {
            switch (level)
            {
                default: return Color.Black;
                case LoggerLevel.Info: return Color.Black;
                case LoggerLevel.Warn: return Color.Goldenrod;
                case LoggerLevel.Error: return Color.Red;
                case LoggerLevel.Debug: return Color.Gray;
            }
        }
    }
}
