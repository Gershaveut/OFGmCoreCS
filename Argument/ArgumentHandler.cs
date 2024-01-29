using System;
using System.Collections.Generic;
using System.Linq;

namespace OFGmCoreCS.Argument
{
    public class ArgumentHandler
    {
        public HashSet<AbstractArgument> arguments;

        public ArgumentHandler(HashSet<AbstractArgument> Arguments)
        {
            this.arguments = Arguments;
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
            foreach (Argument argument in arguments.Cast<Argument>())
            {
                if (argument.name == argumentName)
                    argument.Invoke(argumentValue);
            }
        }

        public string ArgumentsList(string[] args)
        {
            if (args != null)
            {
                string argumentsList = "";

                foreach (string argument in args)
                {
                    argumentsList += ", " + argument;
                }

                return argumentsList.Remove(0, 2);
            }
            return "";
        }

        public void ArgumentsInvoke(string[] args)
        {
            if (args != null)
            {
                foreach (string arg in args)
                {
                    foreach (AbstractArgument argument in arguments)
                    {
                        if (arg.Remove(0, 1) == argument.name)
                        {
                            ((Argument)argument).Invoke(null);
                            break;
                        }
                        else if (arg.Split('=')[0].Remove(0, 2) == argument.name)
                        {
                            if (argument is ArgumentValue)
                                ((ArgumentValue)argument).Invoke(arg.Split('=')[1]);
                            else
                                ((ArgumentValueBool)argument).Invoke(Convert.ToBoolean(arg.Split('=')[1]));

                            break;
                        }
                    }
                }
            }
        }
    }
}
