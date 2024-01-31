using OFGmCoreCS.LoggerSimple;
using System;

namespace OFGmCoreCS.ConsoleSimple
{
    public class CommandHelp : AbstractCommand
    {
        protected readonly CommandHandler commandHandler;

        public CommandHelp(CommandHandler commandHandler) : base("help", "Output of reference information about commands.", new ArgsInput("command", "the command that the user is interested in."))
        {
            this.commandHandler = commandHandler;

            needArgs = 0;
        }

        public override Feedback Execute(string[] args)
        {
            base.Execute(args);

            if (args.Length > 0)
            {
                foreach (AbstractCommand abstractCommand in commandHandler.commands)
                {
                    if (abstractCommand.name == args[0].ToUpper())
                    {
                        string help = description + Environment.NewLine + Environment.NewLine + name;

                        foreach (ArgsInput argInput in abstractCommand.argsInputs)
                        {
                            help += $" [{argInput.name}]";
                        }

                        foreach (ArgsInput argInput in abstractCommand.argsInputs)
                        {
                            help += Environment.NewLine + $"  {argInput.name} - {argInput.description}";
                        }

                        return new Feedback(help, LoggerLevel.Info);
                    }
                }

                return new Feedback("Command not found", LoggerLevel.Warn);
            }
            else
            {
                string commands = $"To get information about a specific command, type {name} {argsInputs[0].name}" + Environment.NewLine;

                foreach (AbstractCommand abstractCommand in commandHandler.commands)
                    commands += Environment.NewLine + $"{abstractCommand.name} - {abstractCommand.description}";

                return new Feedback(commands, LoggerLevel.Info);
            }
        }
    }
}
