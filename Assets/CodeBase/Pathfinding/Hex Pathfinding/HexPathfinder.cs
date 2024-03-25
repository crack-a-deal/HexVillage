using HexagonGrid;
using Pathfinding.BasePathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.HexPathfinding
{
    public class HexPathfinder : MonoBehaviour
    {
        public event Action PathFounded;

        [SerializeField] private HexGridLayoutRenderer layoutRenderer;

        public List<Vector2Int> Path { get; private set; } = new List<Vector2Int>();

        private BasePathfinder<Vector2Int> _pathfinder = new DijkstraPathfinder<Vector2Int>();

        private HexNode[,] _nodesGrid;

        private void Start()
        {
            _nodesGrid = new HexNode[layoutRenderer.GridLayout.Columns, layoutRenderer.GridLayout.Rows];
            _pathfinder.onSuccess = OnSuccessPathFinding;
            _pathfinder.onFailure = OnFailurePathFinding;

            _pathfinder.HeuristicCost = GetManhattanCost;
            _pathfinder.NodeTraversalCost = GetEuclideanCost;

            foreach (Hex hex in layoutRenderer.GridLayout.Grid)
            {
                Vector2Int offsetCoordination = CoordinateConversion.CubeToOffset(new Vector3Int(hex.Q, hex.R, hex.S));
                _nodesGrid[offsetCoordination.x, offsetCoordination.y] = new HexNode(this, layoutRenderer.GetHexagon(offsetCoordination), offsetCoordination);
            }
        }

        public void FindPath(Hexagon start, Hexagon end)
        {
            if (_pathfinder.Status == PathfinderStatus.RUNNING)
            {
                Debug.Log("Pathfinder already running. Cannot find path now");
                return;
            }

            HexNode startNode = new HexNode(this, start, start.Coordinate);
            HexNode endNode = new HexNode(this, end, end.Coordinate);

            _pathfinder.Initialize(startNode, endNode);
            StartCoroutine(Coroutine_FindPathSteps());
        }

        private IEnumerator Coroutine_FindPathSteps()
        {
            while (_pathfinder.Status == PathfinderStatus.RUNNING)
            {
                _pathfinder.Step();
                yield return null;
            }
        }

        public List<BaseNode<Vector2Int>> GetNeighborsNodeList(Hex hex)
        {
            List<Hex> hexes = layoutRenderer.GridLayout.GetNeighborsList(hex);
            List<BaseNode<Vector2Int>> result = new List<BaseNode<Vector2Int>>(6);

            foreach (var item in hexes)
            {
                if (layoutRenderer.CheckHexagon(item.OffsetCoordinates))
                {
                    result.Add(_nodesGrid[item.OffsetCoordinates.x, item.OffsetCoordinates.y]);
                }
            }

            return result;
        }

        private void OnFailurePathFinding()
        {
            Debug.LogError("Cannot find path");
        }

        private void OnSuccessPathFinding()
        {
            PathfinderNode<Vector2Int> node = _pathfinder.CurrentNode;
            List<Vector2Int> reverse_indices = new List<Vector2Int>();
            while (node != null)
            {
                reverse_indices.Add(node.Location.Value);
                node = node.Parent;
            }

            Path.Clear();
            for (int i = reverse_indices.Count - 1; i >= 0; i--)
            {
                Path.Add(new Vector2Int(reverse_indices[i].x, reverse_indices[i].y));
            }

            PathFounded?.Invoke();
        }

        private float GetManhattanCost(Vector2Int a, Vector2Int b)
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }

        private float GetEuclideanCost(Vector2Int a, Vector2Int b)
        {
            return GetCostBetweenTwoCells(a, b);
        }

        private float GetCostBetweenTwoCells(Vector2Int a, Vector2Int b)
        {
            return Mathf.Sqrt(
                    (a.x - b.x) * (a.x - b.x) +
                    (a.y - b.y) * (a.y - b.y)
                );
        }
    }
}