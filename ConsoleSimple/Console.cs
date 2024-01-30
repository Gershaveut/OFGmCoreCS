using OFGmCoreCS.LoggerSimple;

namespace OFGmCoreCS.ConsoleSimple
{
    public class Console
    {
        public Logger logger;
        public CommandHandler commandHandler;

        public Console(Logger logger, CommandHandler commandHandler)
        { 
            this.logger = logger;
            this.commandHandler = commandHandler;
        }

        public void ConsoleWrite(string command)
        {
            logger.LogWrite(commandHandler.ExecuteCommand(command), LoggerLevel.Info);
        }
    }
}
