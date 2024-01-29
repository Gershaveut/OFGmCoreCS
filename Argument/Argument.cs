using System;

namespace OFGmCoreCS.Argument
{
    public class Argument : AbstractArgument
    {
        public Argument(string name, Action action) : base(name, action)
        {
        }

        public override void Invoke(object value)
        {
            ((Action)action).Invoke();
        }
    }
}
