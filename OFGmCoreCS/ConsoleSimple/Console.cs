﻿using OFGmCoreCS.LoggerSimple;

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

        public AbstractCommand.Feedback CommandWrite(string command)
        {
            logger.LogWrite(command, LoggerLevel.Info);
            AbstractCommand.Feedback feedback = commandHandler.ExecuteCommand(command);
            logger.LogWrite(feedback.message, feedback.loggerLevel);

            return feedback;
        }
    }
}