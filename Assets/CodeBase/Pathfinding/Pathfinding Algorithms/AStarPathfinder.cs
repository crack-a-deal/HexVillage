using Pathfinding.BasePathfinding;

namespace Pathfinding.Algorithms
{
    public class AStarPathfinder<T> : BasePathfinder<T>
    {
        protected override void AlgorithmImplementation(BaseNode<T> node)
        {
            if (GetNodePositionInList(CloseList, node.Value) > -1)
                return;

            float currentMovementCost = CurrentNode.CurrentMovementCost + NodeTraversalCost(CurrentNode.ChildNode.Value, node.Value);

            float heuristicCost = HeuristicCost(node.Value, TargetNode.Value);

            int idOList = GetNodePositionInList(OpenList, node.Value);
            if (idOList == -1)
            {
                PathfinderNode<T> n = new PathfinderNode<T>(CurrentNode, node, currentMovementCost, heuristicCost);
                OpenList.Add(n);
            }
            else
            {
                float oldMovementCost = OpenList[idOList].CurrentMovementCost;

                if (currentMovementCost < oldMovementCost)
                {
                    OpenList[idOList].ParrentNode = CurrentNode;
                    OpenList[idOList].SetCurrentMovementCost(currentMovementCost);
                }
            }
        }
    }
}