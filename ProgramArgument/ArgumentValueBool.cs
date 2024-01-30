using System;

namespace OFGmCoreCS.ProgramArgument
{
    public class ArgumentValueBool : AbstractArgument
    {
        public ArgumentValueBool(string name, Action<bool> action) : base(name, action)
        {
        }

        public override void Invoke(object value)
        {
            ((Action<bool>)action).Invoke((bool)value);
        }
    }
}
