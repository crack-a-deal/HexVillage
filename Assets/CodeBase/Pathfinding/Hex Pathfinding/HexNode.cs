using Pathfinding.BasePathfinding;
using System.Collections.Generic;
using UnityEngine;

public class HexNode : BaseNode<Vector2Int>
{
    private HexGridLayoutRenderer _renderer;
    public Hexagon hexagon;
    public bool IsWalkable { get; private set; }
    //public float MovementCost { get; private set; }

    public HexNode(HexGridLayoutRenderer renderer, Hexagon _hexagon, Vector2Int value) : base(value)
    {
        _renderer = renderer;
        hexagon = _hexagon;

        //MovementCost = hexagon.MovementCost;
        IsWalkable = true;
    }

    public override List<BaseNode<Vector2Int>> GetNeighbours()
    {
        return _renderer.GetNeighborsList(this);
        //List<Hexagon> hexagons = _renderer.GetNeighborsList(this);
        //List<Node<Vector2Int>> result = new List<Node<Vector2Int>>();

        //foreach (Hexagon hexagon in hexagons)
        //{
        //    result.Add(new HexNode(_hexagon, _renderer, Value));
        //}
        //Debug.Log(result.Count);
        //return result;
    }
}
