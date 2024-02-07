using OFGmCoreCS.LoggerSimple;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OFGmCoreCS.ConsoleSimple
{
    public class CommandHandler
    {
        public readonly HashSet<AbstractCommand> commands = new HashSet<AbstractCommand>();

        public delegate void RegisterHandler(AbstractCommand command);
        public delegate void ExecuteHandler(AbstractCommand command, string[] args);

        public event RegisterHandler CommandRegistered;
        public event ExecuteHandler CommandExecuted;

        public CommandHandler() 
        {
            commands.Add(new CommandHelp(this));
        }

        public AbstractCommand Register(AbstractCommand command)
        {
            if (commands.All(c => c.name != command.name))
                commands.Add(command);
            else
                throw new CopyCommands();

            CommandRegistered?.Invoke(command);
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
                    CommandExecuted?.Invoke(abstractCommand, args);

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

public class CopyCommands : Exception
{
    public CopyCommands() : base("A command with this name is already registered")
    {

    }
}