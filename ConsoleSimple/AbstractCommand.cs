using System;

namespace OFGmCoreCS.ConsoleSimple
{
    public abstract class AbstractCommand
    {
        public readonly string name;
        public readonly string description;
        public readonly ArgsInput[] argsInputs;

        public AbstractCommand(string name, string description, params ArgsInput[] argsInput)
        {
            this.name = name.ToUpper();
            this.description = description;
            this.argsInputs = argsInput;
        }

        public virtual string Execute(string[] args)
        {
            if (args.Length != argsInputs.Length)
                throw new ArgumentException("The number of arguments is incorrect.");

            return "The number of arguments is correct.";
        }

        public class ArgsInput
        {
            public string name;
            public string description;

            public ArgsInput(string name, string description)
            {
                this.name = $"[{name}]";
                this.description = description;
            }
        }
    }
}
