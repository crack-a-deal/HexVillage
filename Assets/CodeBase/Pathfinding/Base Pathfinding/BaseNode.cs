using System.Collections.Generic;

namespace Pathfinding.BasePathfinding
{
    public abstract class BaseNode<T>
    {
        public T Value { get; private set; }

        public BaseNode(T value)
        {
            Value = value;
        }

        public abstract List<BaseNode<T>> GetNeighbours();
    }
}
