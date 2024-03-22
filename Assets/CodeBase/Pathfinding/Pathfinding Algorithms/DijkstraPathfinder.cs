using Pathfinding.BasePathfinding;

public class DijkstraPathfinder<T> : BasePathfinder<T>
{
    protected override void AlgorithmSpecificImplementation(BaseNode<T> node)
    {
        if (IsInList(ClosedList, node.Value) == -1)
        {
            float G = CurrentNode.GCost + NodeTraversalCost(
                CurrentNode.Location.Value, node.Value);

            float H = 0.0f;

            int idOList = IsInList(OpenList, node.Value);
            if (idOList == -1)
            {
                PathfinderNode<T> n = new PathfinderNode<T>(node, CurrentNode, G, H);
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