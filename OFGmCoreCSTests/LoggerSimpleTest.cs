

using OFGmCoreCS.LoggerSimple;

namespace OFGmCoreCSTests
{
    [TestClass]
    public class LoggerSimpleTest
    {
        [TestMethod]
        public void TestLogger()
        {
            Logger logger = new Logger(new Logger.Properties().Debug().Prefix("Prefix ").Suffix(" Suffix"));
            
            string? logWrittenMessage = null;
            LoggerLevel? logWrittenLevel = null;

            string? logChangeMessage = null;

            logger.LogWritten += (message, level) =>
            {
                logWrittenMessage = message;
                logWrittenLevel = level;
            };
            
            logger.LogChange += (message) => logChangeMessage = message;

            logger.Write("Test", LoggerLevel.Debug);

            Assert.IsTrue(!string.IsNullOrEmpty(logger.Text));

            Assert.IsNotNull(logWrittenMessage);
            Assert.AreEqual(logWrittenLevel, LoggerLevel.Debug);
            
            Assert.IsNotNull(logChangeMessage);
        }
    }
}
