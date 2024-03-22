using Pathfinding.BasePathfinding;

namespace GameAI
{
    namespace PathFinding
    {
        public class AStarPathfinder<T> : BasePathfinder<T>
        {
            protected override void AlgorithmSpecificImplementation(BaseNode<T> cell)
            {
                if (IsInList(ClosedList, cell.Value) == -1)
                {
                    float G = CurrentNode.GCost + NodeTraversalCost(
                        CurrentNode.Location.Value, cell.Value);

                    float H = HeuristicCost(cell.Value, Goal.Value);

                    int idOList = IsInList(OpenList, cell.Value);
                    if (idOList == -1)
                    {
                        PathfinderNode<T> n = new PathfinderNode<T>(cell, CurrentNode, G, H);
                        OpenList.Add(n);
                    }
                    else
                    {
                        float oldG = OpenList[idOList].GCost;
                        if (G < oldG)
                        {
                            OpenList[idOList].Parent = CurrentNode;
                            OpenList[idOList].SetGCost(G);
                        }
                    }
                }
            }
        }
    }
}