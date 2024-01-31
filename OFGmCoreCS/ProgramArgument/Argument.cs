using System;

namespace OFGmCoreCS.ProgramArgument
{
    public interface IArgument
    {
        string Name { get; }
    }

    public class Argument : IArgument
    {
        public readonly string name;

        protected readonly Action action;

        public Argument(string name, Action action)
        {
            this.name = name;
            this.action = action;
        }

        public string Name => name;

        public void Invoke()
        {
            action.Invoke();
        }
    }

    public class Argument<T> : IArgument
    {
        public readonly string name;

        protected readonly Action<T> action;

        public Argument(string name, Action<T> action)
        {
            this.name = name;
            this.action = action;
        }

        public string Name => throw new NotImplementedException();

        public void Invoke(T value)
        {
            action.Invoke(value);
        }
    }
}
