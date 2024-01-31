using OFGmCoreCS.ConsoleSimple;
using OFGmCoreCS.LoggerSimple;
using Console = OFGmCoreCS.ConsoleSimple.Console;

namespace OFGmCoreCSTests
{
    [TestClass]
    public class ConsoleSimpleTest
    {
        [TestMethod]
        public void TestConsole()
        {
            Console console = new Console(new Logger(new Logger.Properties().Debug().MessageMod(false)), new CommandHandler());

            Assert.AreEqual(LoggerLevel.Info, console.CommandWrite("help").loggerLevel);

            Assert.AreEqual(LoggerLevel.Info, console.CommandWrite("help help").loggerLevel);
        }
    }
}
