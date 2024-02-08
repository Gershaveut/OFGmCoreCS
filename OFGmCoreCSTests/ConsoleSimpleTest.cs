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
            Console console = new Console(new Logger(new Logger.Properties().Debug().MessageMod(false)));

            Assert.AreEqual(LoggerLevel.Info, console.CommandWrite("help").loggerLevel);
            Assert.AreEqual(LoggerLevel.Info, console.CommandWrite("help help").loggerLevel);
        }

        [TestMethod]
        public void TestCommandRegister()
        {
            CommandHandler handler = new CommandHandler();

            handler.Register(new TestCommand());

            handler.ExecuteCommand("test");
        }

        class TestCommand : AbstractCommand
        {
            public TestCommand() : base("test", "Test command.", null)
            {

            }
        }
    }
}
