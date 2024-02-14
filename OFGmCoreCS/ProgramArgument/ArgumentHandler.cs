using OFGmCoreCS.Util;
using System;
using System.Collections.Generic;

namespace OFGmCoreCS.ProgramArgument
{
    public class ArgumentHandler
    {
        public readonly HashSet<IArgument> arguments;

        public delegate void ArgumentType(IArgument argument, string arg);
        public ArgumentType argumentType;

        public delegate void InvokeHandler(string argumentName, object value);

        public event InvokeHandler ArgumentInvoked;

        public ArgumentHandler(HashSet<IArgument> arguments)
        {
            this.arguments = arguments;

            argumentType += IsString;
            argumentType += IsBool;
            argumentType += IsInt;
            argumentType += IsDouble;
        }
        
        public static string ArgumentsList(string[] args) => args != null ? string.Join(", ", args) : "";

        public static void IsString(IArgument argument, string arg) => ArgumentInvoke(argument, arg);

        public static void IsBool(IArgument argument, string arg)
        {
            if (argument is Argument<bool> argumentType)
                ArgumentInvoke(argumentType, Convert.ToBoolean(arg));
        }

        public static void IsInt(IArgument argument, string arg)
        {
            if (argument is Argument<int> argumentType)
                ArgumentInvoke(argumentType, Convert.ToInt32(arg));
        }

        public static void IsDouble(IArgument argument, string arg)
        {
            if (argument is Argument<double> argumentType)
                ArgumentInvoke(argumentType, Convert.ToDouble(arg, Utils.simpleProvider));
        }

        public void ArgumentInvoke(string argumentName)
        {
            foreach (IArgument argument in arguments)
            {
                if (argument.Name == argumentName)
                    if (argument is Argument argumentType)
                        argumentType.Invoke();
            }

            ArgumentInvoked?.Invoke(argumentName, null);
        }

        public void ArgumentInvoke<T>(string argumentName, T argumentValue)
        {
            foreach (IArgument argument in arguments)
            {
                if (argument.Name == argumentName)
                    if (argument is Argument<T> argumentType)
                        argumentType.Invoke(argumentValue);
            }

            ArgumentInvoked?.Invoke(argumentName, argumentValue);
        }

        public static void ArgumentInvoke<T>(IArgument argument, T arg)
        {
            if (argument is Argument<T> argumentType)
                argumentType.Invoke(arg);
        }

        public static void ArgumentInvoke<T>(Argument<T> argument, T arg) => argument.Invoke(arg);

        public static void ArgumentInvoke(Argument argument) => argument.Invoke();

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
                            string value = null;

                            if (arg.Contains("="))
                                value = arg.Split('=')[1];
                            
                            if (arg.Substring(1) == argument.Name)
                                ((Argument)argument).Invoke();
                            else if (arg.Split('=')[0].Substring(2) == argument.Name)
                                argumentType(argument, arg.Split('=')[1]);
                            else
                                continue;

                            ArgumentInvoked?.Invoke(argument.Name, value);
                            break;
                        }
                    }
                }
            }
        }
    }
}
