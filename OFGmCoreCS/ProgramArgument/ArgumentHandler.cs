using System;
using System.Collections.Generic;

namespace OFGmCoreCS.ProgramArgument
{
    public class ArgumentHandler
    {
        public readonly HashSet<IArgument> arguments;

        public delegate void ArgumentType(IArgument argument, string arg);
        public ArgumentType argumentType;

        public ArgumentHandler(HashSet<IArgument> arguments)
        {
            this.arguments = arguments;

            argumentType += IsString;
            argumentType += IsBool;
            argumentType += IsInt;
            argumentType += IsDouble;
        }

        public void ArgumentInvoke(string argumentName)
        {
            foreach (Argument argument in arguments)
            {
                if (argument.name == argumentName)
                    argument.Invoke();
            }
        }

        public void ArgumentInvoke(string argumentName, string argumentValue)
        {
            foreach (Argument<string> argument in arguments)
            {
                if (argument.name == argumentName)
                    argument.Invoke(argumentValue);
            }
        }

        public void ArgumentInvoke(string argumentName, bool argumentValue)
        {
            foreach (Argument<bool> argument in arguments)
            {
                if (argument.name == argumentName)
                    argument.Invoke(argumentValue);
            }
        }

        public static string ArgumentsList(string[] args) => args != null ? string.Join(", ", args) : "";
        
        public static void IsString(IArgument argument, string arg) => ArgumentInvoke(argument, arg);
        public static void IsBool(IArgument argument, string arg) => ArgumentInvoke(argument, Convert.ToBoolean(arg));
        public static void IsInt(IArgument argument, string arg) => ArgumentInvoke(argument, Convert.ToInt64(arg));
        public static void IsDouble(IArgument argument, string arg) => ArgumentInvoke(argument, Convert.ToDouble(arg));

        private static void ArgumentInvoke<T>(IArgument argument, T arg)
        {
            if (argument is Argument<T> argumentType)
                argumentType.Invoke(arg);
        }

        public static void ArgumentInvoke<T>(Action<T> argument, T arg) => argument.Invoke(arg);

        public static void ArgumentInvoke(Action argument) => argument.Invoke();

        public void ArgumentsInvoke(string[] args)
        {
            if (args != null)
            {
                foreach (string arg in args)
                {
                    if (arg.Length > 1)
                    {
                        foreach (IArgument argument in arguments)
                        {
                            if (arg.Substring(1) == argument.Name)
                                ((Argument)argument).Invoke();
                            else if (arg.Split('=')[0].Substring(2) == argument.Name)
                                argumentType(argument, arg.Split('=')[1]);
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
