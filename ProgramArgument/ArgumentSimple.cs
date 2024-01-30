using System;

namespace OFGmCoreCS.ProgramArgument
{
    public class ArgumentSimple : AbstractArgument
    {
        public ArgumentSimple(string name, Action action) : base(name, action)
        {
        }

        public override void Invoke(object value)
        {
            ((Action)action).Invoke();
        }
    }
}
