public class PathfinderNode
{
    public PathfinderNode Parent { get; set; }
    public Node Location { get; private set; }

    public float Fcost { get; private set; }
    public float GCost { get; private set; }
    public float Hcost { get; private set; }


    public PathfinderNode(PathfinderNode parent, Node location, float gCost, float hCost)
    {
        Parent = parent;
        Location = location;
        Hcost = hCost;
        SetGCost(gCost);
    }

    public void SetGCost(float c)
    {
        GCost = c;
        Fcost = GCost + Hcost;
    }
}