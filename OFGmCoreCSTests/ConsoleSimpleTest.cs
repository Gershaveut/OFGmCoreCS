﻿using OFGmCoreCS.ConsoleSimple;
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

        [TestMethod]
        public void TestCommandsHistory()
        {
            CommandHandler handler = new CommandHandler();

            handler.ExecuteCommand("help 1");
            handler.ExecuteCommand("help 2");
            handler.ExecuteCommand("help 3");
            handler.ExecuteCommand("help 4");
            handler.ExecuteCommand("help 5");

            System.Console.WriteLine(Utils.AssertReturn("help 5", handler.GetPrevious()));
            System.Console.WriteLine(Utils.AssertReturn("help 4", handler.GetPrevious()));
            System.Console.WriteLine(Utils.AssertReturn("help 3", handler.GetPrevious()));

            System.Console.WriteLine(Utils.AssertReturn("help 4", handler.GetNext()));
            System.Console.WriteLine(Utils.AssertReturn("help 5", handler.GetNext()));
            System.Console.WriteLine(Utils.AssertReturn("help 1", handler.GetNext()));
        }

        class TestCommand : AbstractCommand
        {
            public TestCommand() : base("test", "Test command.", null)
            {

            }
        }
    }
}
