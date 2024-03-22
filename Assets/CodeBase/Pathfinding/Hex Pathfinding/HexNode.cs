using Pathfinding.BasePathfinding;
using System.Collections.Generic;
using UnityEngine;

public class HexNode : BaseNode<Vector2Int>
{
    public Hexagon Hexagon { get; private set; }
    public bool IsWalkable { get; private set; }
    //public float MovementCost { get; private set; }
    private HexGridLayoutRenderer _renderer;

    public HexNode(HexGridLayoutRenderer renderer, Hexagon hexagon, Vector2Int value) : base(value)
    {
        _renderer = renderer;
        Hexagon = hexagon;

        //MovementCost = hexagon.MovementCost;
        IsWalkable = hexagon.IsWalkable;
    }

    public override List<BaseNode<Vector2Int>> GetNeighbours()
    {
        return _renderer.GetNeighborsList(this);
    }
}
