using HexagonGrid;
using Pathfinding.BasePathfinding;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.HexPathfinding
{
    public class HexNode : BaseNode<Vector2Int>
    {
        public Hexagon Hexagon { get; private set; }
        public bool IsWalkable { get; private set; }

        private HexPathfinder _pathfinder;

        public HexNode(HexPathfinder pathfinder, Hexagon hexagon, Vector2Int value) : base(value)
        {
            _pathfinder = pathfinder;

            Hexagon = hexagon;
            IsWalkable = hexagon.IsWalkable;
        }

        public override List<BaseNode<Vector2Int>> GetNeighbours()
        {
            return _pathfinder.GetNeighborsNodeList(Hexagon.HexData);
        }
    }
}