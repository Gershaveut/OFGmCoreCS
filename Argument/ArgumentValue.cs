using System;

namespace OFGmCoreCS.Argument
{
    public class ArgumentValue : AbstractArgument
    {
        public ArgumentValue(string name, Action<string> action) : base(name, action)
        {
        }

        public override void Invoke(object value)
        {
            ((Action<string>)action).Invoke((string)value);
        }
    }
}
