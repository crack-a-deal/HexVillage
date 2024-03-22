using System.Collections.Generic;

namespace Pathfinding.BasePathfinding
{
    public abstract class BasePathfinder<T>
    {
        public PathfinderStatus Status { get; private set; } = PathfinderStatus.NOT_INITIALIZED;
        public BaseNode<T> StartNode { get; private set; }
        public BaseNode<T> TargetNode { get; private set; }
        public PathfinderNode<T> CurrentNode { get; private set; }

        public delegate float CostFunction(T a, T b);
        public CostFunction HeuristicCost { get; set; }
        public CostFunction NodeTraversalCost { get; set; }

        protected List<PathfinderNode<T>> OpenList = new List<PathfinderNode<T>>();
        protected List<PathfinderNode<T>> CloseList = new List<PathfinderNode<T>>();

        public bool Initialize(BaseNode<T> startNode, BaseNode<T> targetNode)
        {
            if (Status == PathfinderStatus.RUNNING)
            {
                return false;
            }

            Reset();
            StartNode = startNode;
            TargetNode = targetNode;

            Status = PathfinderStatus.RUNNING;
            return true;
        }

        public void Reset()
        {
            if (Status == PathfinderStatus.RUNNING)
            {
                return;
            }

            CurrentNode = null;
            Status = PathfinderStatus.NOT_INITIALIZED;
        }

        public PathfinderStatus Step()
        {
            CloseList.Add(CurrentNode);

            if (OpenList.Count == 0)
            {
                Status = PathfinderStatus.FAILURE;
                return Status;
            }

            CurrentNode = GetLeastCostNode(OpenList);

            OpenList.Remove(CurrentNode);

            if (EqualityComparer<T>.Default.Equals(CurrentNode.ChildNode.Value, TargetNode.Value))
            {
                Status = PathfinderStatus.SUCCSESS;
                return Status;
            }

            List<BaseNode<T>> neighbours = CurrentNode.ChildNode.GetNeighbours();

            foreach(BaseNode<T> node in neighbours)
            {
                AlgorithmImplementation(node);
            }

            Status = PathfinderStatus.RUNNING;
            return Status;
        }

        private PathfinderNode<T> GetLeastCostNode(List<PathfinderNode<T>> list)
        {
            int best_index = 0;
            float best_priority = list[0].TotalCost;
            for (int i = 1; i < list.Count; i++)
            {
                if (best_priority > list[i].TotalCost)
                {
                    best_priority = list[i].TotalCost;
                    best_index = i;
                }
            }
            PathfinderNode<T> n = list[best_index];
            return n;
        }

        protected int GetNodePositionInList(List<PathfinderNode<T>> list, T targetNode)
        {
            for(int i=0; i<list.Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(list[i].ChildNode.Value, targetNode))
                    return i;
            }
            return -1;
        }

        protected abstract void AlgorithmImplementation(BaseNode<T> node);
    }
}
