using System;
using System.Collections.Generic;

namespace BotTemplate.Engines
{
    public abstract class State : IComparable<State>, IComparer<State>
    {
        public abstract int Priority { get; }

        public abstract string Name { get; }

        public abstract bool NeedToRun { get; }

        public abstract void Run();

        public int CompareTo(State other)
        {
            return -Priority.CompareTo(other.Priority);
        }

        public int Compare(State x, State y)
        {
            return -x.Priority.CompareTo(y.Priority);
        }
    }
}
