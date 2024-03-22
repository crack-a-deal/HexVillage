namespace Pathfinding.BasePathfinding
{
    public class PathfinderNode<T>
    {
        public PathfinderNode<T> Parent { get; set; }

        public BaseNode<T> Location { get; private set; }

        public float Fcost { get; private set; }
        public float GCost { get; private set; }
        public float Hcost { get; private set; }


        public PathfinderNode(BaseNode<T> location, PathfinderNode<T> parent, float gCost, float hCost)
        {
            Location = location;
            Parent = parent;
            Hcost = hCost;
            SetGCost(gCost);
        }

        public void SetGCost(float c)
        {
            GCost = c;
            Fcost = GCost + Hcost;
        }
    }
}