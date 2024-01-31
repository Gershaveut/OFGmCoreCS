using OFGmCoreCS.LoggerSimple;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OFGmCoreCS.ConsoleSimple
{
    public class CommandHandler
    {
        public readonly HashSet<AbstractCommand> commands = new HashSet<AbstractCommand>();

        public CommandHandler() 
        {
            commands.Add(new CommandHelp(this));
        }

        public AbstractCommand Register(AbstractCommand command)
        {
            commands.Add(command);
            return command;
        }

        public AbstractCommand.Feedback ExecuteCommand(string command)
        {
            string[] args = command.ToLower().Split(' ');
            string commandName = args[0].ToUpper();
            args = args.Skip(1).ToArray();

            foreach (AbstractCommand abstractCommand in commands)
            {
                if (abstractCommand.name == commandName)
                {
                    try
                    {
                        AbstractCommand.Feedback feedback = abstractCommand.Execute(args);
                        return new AbstractCommand.Feedback(feedback.message + Environment.NewLine, feedback.loggerLevel);
                    }
                    catch (Exception ex)
                    {
                        return new AbstractCommand.Feedback(ex.Message, LoggerLevel.Error);
                    }
                }
            }

            return new AbstractCommand.Feedback("Command not found" + Environment.NewLine, LoggerLevel.Warn);
        }
    }
}
