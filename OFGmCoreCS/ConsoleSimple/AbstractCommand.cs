using OFGmCoreCS.LoggerSimple;
using System;

namespace OFGmCoreCS.ConsoleSimple
{
    public abstract class AbstractCommand
    {
        public readonly string name;
        public readonly string description;
        public readonly ArgsInput[] argsInputs;
        protected int needArgs;

        public AbstractCommand(string name, string description, params ArgsInput[] argsInputs)
        {
            this.name = name.ToUpper();
            this.description = description;
            this.argsInputs = argsInputs;
            needArgs = argsInputs != null ? argsInputs.Length : 0;
        }

        public AbstractCommand(string name, string description) : this(name, description, null)
        {
        }

        public virtual Feedback Execute(string[] args)
        {
            if (args.Length < needArgs)
                throw new ArgumentException("The number of arguments is incorrect.");

            return new Feedback("The number of arguments is correct.", LoggerLevel.Info);
        }

        public class ArgsInput
        {
            public string name;
            public string description;

            public ArgsInput(string name, string description)
            {
                this.name = name;
                this.description = description;
            }
        }

        public struct Feedback
        {
            public static Feedback empty = new Feedback("", LoggerLevel.Info);

            public string message;
            public LoggerLevel loggerLevel;

            public Feedback(string message, LoggerLevel loggerLevel)
            {
                this.message = message;
                this.loggerLevel = loggerLevel;
            }

            public Feedback(string message) : this(message, LoggerLevel.Info)
            {
            }
        }
    }
}
