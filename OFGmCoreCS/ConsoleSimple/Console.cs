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

        public Console(Logger logger) : this(logger, new CommandHandler())
        {
            
        }

        public Console() : this(new Logger(new Logger.Properties()))
        {
            
        }

        public AbstractCommand.Feedback CommandWrite(string command)
        {
            logger.Write(command, LoggerLevel.Info);
            AbstractCommand.Feedback feedback = commandHandler.ExecuteCommand(command);
            logger.Write(feedback.message, feedback.loggerLevel);

            return feedback;
        }
    }
}
