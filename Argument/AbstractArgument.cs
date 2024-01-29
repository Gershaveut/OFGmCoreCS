using System;

namespace OFGmCoreCS.Argument
{
    public abstract class AbstractArgument
    {
        public string name;

        protected readonly object action;

        public AbstractArgument(string name, object action)
        {
            this.name = name;
            this.action = action;
        }

        public abstract void Invoke(object value);
    }
}
