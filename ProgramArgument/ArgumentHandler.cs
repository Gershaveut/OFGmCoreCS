using System;
using System.Collections.Generic;

namespace OFGmCoreCS.ProgramArgument
{
    public class ArgumentHandler
    {
        public readonly HashSet<AbstractArgument> arguments;

        public ArgumentHandler(HashSet<AbstractArgument> arguments)
        {
            this.arguments = arguments;
        }

        public void ArgumentInvoke(string argumentName)
        {
            foreach (AbstractArgument argument in arguments)
            {
                if (argument.name == argumentName)
                    argument.Invoke(null);
            }
        }

        public void ArgumentInvoke(string argumentName, string argumentValue)
        {
            foreach (AbstractArgument argument in arguments)
            {
                if (argument.name == argumentName)
                    argument.Invoke(argumentValue);
            }
        }

        public string ArgumentsList(string[] args)
        {
            return args != null ? string.Join(", ", args) : "";
        }

        public void ArgumentsInvoke(string[] args)
        {
            if (args != null)
            {
                foreach (string arg in args)
                {
                    if (arg.Length > 1)
                    {
                        foreach (AbstractArgument argument in arguments)
                        {
                            if (arg.Substring(1) == argument.name)
                                argument.Invoke(null);
                            else if (arg.Split('=')[0].Substring(2) == argument.name)
                            {
                                if (argument is ArgumentValue)
                                    ((ArgumentValue)argument).Invoke(arg.Split('=')[1]);
                                else if (argument is ArgumentValueBool)
                                    ((ArgumentValueBool)argument).Invoke(Convert.ToBoolean(arg.Split('=')[1]));
                            }
                            else
                                continue;

                            break;
                        }
                    }
                }
            }
        }
    }
}