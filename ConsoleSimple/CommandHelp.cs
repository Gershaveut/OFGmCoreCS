using System;

namespace OFGmCoreCS.ConsoleSimple
{
    public class CommandHelp : AbstractCommand
    {
        protected readonly CommandHandler commandHandler;

        public CommandHelp(CommandHandler commandHandler) : base("help", "Output of reference information about commands.", new ArgsInput("command", "the command that the user is interested in."))
        {
            this.commandHandler = commandHandler;
        }

        public override string Execute(string[] args)
        {
            base.Execute(args);

            if (args.Length > 0)
            {
                foreach (AbstractCommand abstractCommand in commandHandler.commands)
                {
                    if (abstractCommand.name == args[0])
                    {
                        string help = description + Environment.NewLine + this.name;

                        foreach (ArgsInput argInput in abstractCommand.argsInputs)
                        {
                            help += $" [{argInput.name}]";
                        }

                        foreach (ArgsInput argInput in abstractCommand.argsInputs)
                        {
                            help += Environment.NewLine + $"  {argInput.name} - {argInput.description}";
                        }

                        return help;
                    }
                }

                return "Command not found";
            }
            else
            {
                string commands = $"To get information about a specific command, type {name} {argsInputs[0].name}";

                foreach (AbstractCommand abstractCommand in commandHandler.commands)
                    commands += Environment.NewLine + $"{abstractCommand.name} - {abstractCommand.description}";

                return commands;
            }
        }
    }
}
