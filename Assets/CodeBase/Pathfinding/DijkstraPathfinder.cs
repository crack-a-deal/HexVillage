public class DijkstraPathfinder : Pathfinder
{
    protected override void PathfindingImplementation(Node node)
    {
        if (IsInList(ClosedList, node.Value) == -1)
        {
            float G = Current.GCost + NodeTraversalCost(Current.Location.Value, node.Value);
            float H = 0.0f;
            int idOpenList = IsInList(OpenList, node.Value);
            if (idOpenList == -1)
            {
                PathfinderNode n = new PathfinderNode(Current, node, G, H);
                OpenList.Add(n);
            }
            else
            {
                float oldG = OpenList[idOpenList].GCost;
                if (G < oldG)
                {
                    OpenList[idOpenList].Parent = Current;
                    OpenList[idOpenList].SetGCost(G);
                }
            }
        }
    }
}
