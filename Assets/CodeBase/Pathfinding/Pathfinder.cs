using System.Collections.Generic;

public abstract class Pathfinder
{
    public delegate float CostFunction(int a, int b);
    public CostFunction HeuristicCost { get; set; }
    public CostFunction NodeTraversalCost { get; set; }  

    public Node Start { get; private set; }
    public Node Goal { get; private set; }


    public PathfinderNode Current { get; private set; }

    //TODO: use PriorityQueue
    protected List<PathfinderNode> OpenList = new List<PathfinderNode>();
    protected List<PathfinderNode> ClosedList = new List<PathfinderNode>();

    protected PathfinderNode GetLeastCostNode(List<PathfinderNode> list)
    {
        int bestIndex = 0;
        float bestPriority = list[0].Fcost;
        for (int i = 1; i < list.Count; i++)
        {
            if (bestPriority > list[i].Fcost)
            {
                bestPriority = list[i].Fcost;
                bestIndex = i;
            }
        }
        return list[bestIndex];
    }

    protected int IsInList(List<PathfinderNode> list, int node)
    {
        for (int i = 0; i < list.Count; ++i)
        {
            if (EqualityComparer<int>.Default.Equals(list[i].Location.Value, node))
                return i;
        }
        return -1;
    }

    public bool Initialize(Node start, Node goal)
    {
        Reset();

        Start = start;
        Goal = goal;

        float H = HeuristicCost(Start.Value, Goal.Value);

        PathfinderNode root = new PathfinderNode(null, Start, 0f, H);

        OpenList.Add(root);
        Current = root;

        return true;
    }

    public void Step()
    {
        ClosedList.Add(Current);

        if (OpenList.Count == 0)
            return;

        Current = GetLeastCostNode(OpenList);

        OpenList.Remove(Current);

        List<Node> neighbours = Current.Location.GetNeighbours();

        foreach (Node node in neighbours)
        {
            PathfindingImplementation(node);
        }
    }

    protected void Reset()
    {
        Current = null;
        OpenList.Clear();
        ClosedList.Clear();
    }

    abstract protected void PathfindingImplementation(Node node);
}
