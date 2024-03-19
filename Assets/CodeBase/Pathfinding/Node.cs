using System.Collections.Generic;

public abstract class Node
{
    public int Value { get; private set; }
    public Node(int value)
    {
        Value = value;
    }
    public abstract List<Node> GetNeighbours();
}
