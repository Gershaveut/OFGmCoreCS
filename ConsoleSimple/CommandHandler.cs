using System;
using System.Collections.Generic;
using System.Linq;

namespace OFGmCoreCS.ConsoleSimple
{
    public class CommandHandler
    {
        public readonly HashSet<AbstractCommand> commands = new HashSet<AbstractCommand>();

        public CommandHandler() { }

        public AbstractCommand Register(AbstractCommand command)
        {
            commands.Add(command);
            return command;
        }

        public string ExecuteCommand(string command)
        {
            string[] args = command.Split(' ');
            string commandName = args[0].ToUpper();
            args = args.Skip(1).ToArray();

            foreach (AbstractCommand abstractCommand in commands)
            {
                if (abstractCommand.name == commandName)
                {
                    try
                    {
                        return abstractCommand.Execute(args) + Environment.NewLine;
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
            }

            return "Command not found" + Environment.NewLine;
        }
    }
}
