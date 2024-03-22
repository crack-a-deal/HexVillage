using System.Collections.Generic;

namespace Pathfinding.BasePathfinding
{
    public abstract class BasePathfinder<T>
    {
        public delegate float CostFunction(T a, T b);
        public CostFunction HeuristicCost { get; set; }
        public CostFunction NodeTraversalCost { get; set; }

        public PathfinderStatus Status { get; private set; } = PathfinderStatus.NOT_INITIALIZED;

        public BaseNode<T> Start { get; private set; }
        public BaseNode<T> Goal { get; private set; }

        public PathfinderNode<T> CurrentNode { get; private set; }

        public delegate void DelegateNoArgument();
        public DelegateNoArgument onFailure;
        public DelegateNoArgument onSuccess;

        protected List<PathfinderNode<T>> OpenList = new List<PathfinderNode<T>>();
        protected List<PathfinderNode<T>> ClosedList = new List<PathfinderNode<T>>();

        public bool Initialize(BaseNode<T> start, BaseNode<T> goal)
        {
            if (Status == PathfinderStatus.RUNNING)
            {
                return false;
            }

            Reset();

            Start = start;
            Goal = goal;

            float H = HeuristicCost(Start.Value, Goal.Value);

            PathfinderNode<T> root = new PathfinderNode<T>(Start, null, 0f, H);

            OpenList.Add(root);

            CurrentNode = root;

            Status = PathfinderStatus.RUNNING;

            return true;
        }
        
        public PathfinderStatus Step()
        {
            ClosedList.Add(CurrentNode);

            if (OpenList.Count == 0)
            {
                Status = PathfinderStatus.FAILURE;
                onFailure?.Invoke();
                return Status;
            }

            CurrentNode = GetLeastCostNode(OpenList);

            OpenList.Remove(CurrentNode);

            if (EqualityComparer<T>.Default.Equals(
                CurrentNode.Location.Value, Goal.Value))
            {
                Status = PathfinderStatus.SUCCESS;
                onSuccess?.Invoke();
                return Status;
            }

            List<BaseNode<T>> neighbours = CurrentNode.Location.GetNeighbours();

            foreach (BaseNode<T> cell in neighbours)
            {
                AlgorithmSpecificImplementation(cell);
            }

            Status = PathfinderStatus.RUNNING;
            return Status;
        }

        protected void Reset()
        {
            if (Status == PathfinderStatus.RUNNING)
            {
                return;
            }

            CurrentNode = null;

            OpenList.Clear();
            ClosedList.Clear();

            Status = PathfinderStatus.NOT_INITIALIZED;
        }

        protected PathfinderNode<T> GetLeastCostNode(List<PathfinderNode<T>> myList)
        {
            int best_index = 0;
            float best_priority = myList[0].Fcost;
            for (int i = 1; i < myList.Count; i++)
            {
                if (best_priority > myList[i].Fcost)
                {
                    best_priority = myList[i].Fcost;
                    best_index = i;
                }
            }

            PathfinderNode<T> n = myList[best_index];
            return n;
        }

        protected int IsInList(List<PathfinderNode<T>> myList, T cell)
        {
            for (int i = 0; i < myList.Count; ++i)
            {
                if (EqualityComparer<T>.Default.Equals(myList[i].Location.Value, cell))
                    return i;
            }
            return -1;
        }

        abstract protected void AlgorithmSpecificImplementation(BaseNode<T> cell);
    }
}