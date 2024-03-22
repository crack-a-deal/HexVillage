namespace Pathfinding.BasePathfinding
{
    public class PathfinderNode<T>
    {
        public PathfinderNode<T> ParrentNode { get; set; }
        public BaseNode<T> ChildNode { get; set; }

        public float TotalCost { get; private set; }
        public float CurrentMovementCost { get; private set; }
        public float HeuristicCost { get; private set; }

        public PathfinderNode(PathfinderNode<T> parrentNode, BaseNode<T> childNode, float currentMovementCost, float heuristicCost)
        {
            ParrentNode = parrentNode;
            ChildNode = childNode;

            CurrentMovementCost = currentMovementCost;
            HeuristicCost = heuristicCost;
            TotalCost = CurrentMovementCost + HeuristicCost;
        }

        public void SetCurrentMovementCost(float currentMovementCost)
        {
            CurrentMovementCost = currentMovementCost;
            TotalCost = CurrentMovementCost + HeuristicCost;
        }
    }
}