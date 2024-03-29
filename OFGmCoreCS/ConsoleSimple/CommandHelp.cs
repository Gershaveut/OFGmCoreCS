﻿using OFGmCoreCS.LoggerSimple;
using OFGmCoreCS.Util;
using System;
using System.Linq;

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
                foreach (AbstractCommand abstractCommand in commandHandler.Commands)
                {
                    if (abstractCommand.name == args[0].ToUpper())
                    {
                        string help;

                        if (abstractCommand.argsInputs != null)
                        {
                            help = abstractCommand.description + Utils.LineSeparator + abstractCommand.name;

                            foreach (ArgsInput argInput in abstractCommand.argsInputs)
                            {
                                help += $" [{argInput.name}]";
                            }

                            foreach (ArgsInput argInput in abstractCommand.argsInputs)
                            {
                                char[] separator = new char[] { '[', '{', '<', '(' };
                                string name = argInput.name.Split(separator)[0];
                                help += Environment.NewLine + $"  {name} {string.Concat(Enumerable.Repeat(" ", abstractCommand.argsInputs.Max(a => a.name.Split(separator)[0].Length) - name.Length))}- {argInput.description}";
                            }
                        }
                        else
                            help = abstractCommand.description + Environment.NewLine;

                        return new Feedback(help, LoggerLevel.Info);
                    }
                }

                return new Feedback("Command not found", LoggerLevel.Warn);
            }
            else
            {
                string commands = $"To get information about a specific command, type {name} {argsInputs[0].name}" + Environment.NewLine;

                foreach (AbstractCommand abstractCommand in commandHandler.Commands)
                    commands += Environment.NewLine + $"{abstractCommand.name} {string.Concat(Enumerable.Repeat(" ", commandHandler.Commands.Max(c => c.name.Length) - abstractCommand.name.Length))}- {abstractCommand.description}";

                return new Feedback(commands, LoggerLevel.Info);
            }
        }
    }
}
