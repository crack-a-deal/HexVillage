using Pathfinding.BasePathfinding;
using Pathfinding.HexPathfinding;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HexagonRenderer))]
public class HexGridLayoutRenderer : MonoBehaviour
{
    public event Action UpdateGridLayout;

    [Header("Grid Size")]
    [SerializeField] private int columnCount;
    [SerializeField] private int rowCount;

    [Space]
    [SerializeField] private HexagonRenderer hexagonRenderer;
    private HexGridLayout _gridLayout;

    public Hexagon[,] Grid => _hexagonsGrid;
    private Hexagon[,] _hexagonsGrid;

    private HexNode[,] _nodesGrid;

    private void Awake()
    {
        _gridLayout = new HexGridLayout(columnCount, rowCount);
        _nodesGrid = new HexNode[columnCount, rowCount];
        _gridLayout.CreateLayoutGrid();
        RenderGrid();
    }

    private void RenderGrid()
    {
        _hexagonsGrid = new Hexagon[columnCount, rowCount];

        foreach (Hex hex in _gridLayout.Grid)
        {
            Vector2Int offsetCoordination = CoordinateConversion.CubeToOffset(new Vector3Int(hex.Q, hex.R, hex.S));
            Hexagon hexagon = hexagonRenderer.InitHex(offsetCoordination);
            hexagon.HexData = hex;
            hexagon.transform.position = GetHexagonPositionFromCoordinates(offsetCoordination) + (Vector2)transform.position;
            _hexagonsGrid[offsetCoordination.x, offsetCoordination.y] = hexagon;
            _nodesGrid[offsetCoordination.x, offsetCoordination.y] = new HexNode(this, _hexagonsGrid[offsetCoordination.x, offsetCoordination.y], offsetCoordination);
        }
        UpdateGridLayout?.Invoke();
    }

    private Vector2 GetHexagonPositionFromCoordinates(Vector2Int coordinates)
    {
        int column = coordinates.x;
        int row = coordinates.y;

        bool shoutOffset = column % 2 != 0;
        float offset = shoutOffset ? 0.45f : 0f;

        float rowPosition = row + offset - (0.1f * row);
        float colPosition = column - (column * 0.2f);
        rowPosition *= /*isEvenColum ? 1 :*/ -1;
        return new Vector2(colPosition, rowPosition);
    }

    public void ClearFrame()
    {
        foreach (Hexagon hex in _hexagonsGrid)
        {
            hexagonRenderer.ClearToBaseColor(hex);
        }
        UpdateGridLayout?.Invoke();
    }

    public void ClearToDefault()
    {
        foreach (Hexagon hex in _hexagonsGrid)
        {
            hexagonRenderer.ClearToDefaultColor(hex);
        }
        UpdateGridLayout?.Invoke();
    }

    public void ChangeTempHexColor(Hexagon hex, Color color)
    {
        hexagonRenderer.ChangeTempHexagonColor(hex, color);
        UpdateGridLayout?.Invoke();
    }

    public void ChangeBaseHexColor(Hexagon hex, Color color)
    {
        hexagonRenderer.ChangeBaseHexagonColor(hex, color);
        UpdateGridLayout?.Invoke();
    }

    public Color GetColor(HexagonType type) => hexagonRenderer.GetColorFromType(type);

    public void DrawLine(List<Vector2Int> path, Color color)
    {
        ClearFrame();
        foreach (var hex in path)
        {
            // TODO: fix IndexOutOfRangeException
            ChangeTempHexColor(_hexagonsGrid[hex.x, hex.y], color);
        }
    }
    public void DrawBaseLine(List<Vector2Int> path, Color color)
    {
        foreach (var hex in path)
        {
            ChangeBaseHexColor(_hexagonsGrid[hex.x, hex.y], color);
        }
    }

    public List<BaseNode<Vector2Int>> GetNeighborsList(HexNode hexNode)
    {
        List<BaseNode<Vector2Int>> result = new List<BaseNode<Vector2Int>>(6);

        if (hexNode.Hexagon.Coordinate.x % 2 == 0)
        {
            GetEvenNeigbors(hexNode.Hexagon.Coordinate, result);
        }
        else
        {
            GetOddNeigbors(hexNode.Hexagon.Coordinate, result);
        }

        result.RemoveAll(item => item == null);
        return result;
    }

    private void GetEvenNeigbors(Vector2Int hexagonCoord, List<BaseNode<Vector2Int>> neighors)
    {
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x + 1, hexagonCoord.y)));
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x + 1, hexagonCoord.y - 1)));
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x, hexagonCoord.y - 1)));
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x - 1, hexagonCoord.y - 1)));
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x - 1, hexagonCoord.y)));
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x, hexagonCoord.y + 1)));
    }

    private void GetOddNeigbors(Vector2Int hexagonCoord, List<BaseNode<Vector2Int>> neighors)
    {
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x + 1, hexagonCoord.y + 1)));
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x + 1, hexagonCoord.y)));
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x, hexagonCoord.y - 1)));
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x - 1, hexagonCoord.y)));
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x - 1, hexagonCoord.y + 1)));
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x, hexagonCoord.y + 1)));
    }

    private BaseNode<Vector2Int> GetNeighbor(Vector2Int hexagonCoord)
    {
        if (hexagonCoord.x < 0 || hexagonCoord.x >= columnCount)
        {
            return null;
        }
        if (hexagonCoord.y < 0 || hexagonCoord.y >= rowCount)
        {
            return null;
        }

        if (!_hexagonsGrid[hexagonCoord.x, hexagonCoord.y].IsWalkable)
        {
            return null;
        }

        return _nodesGrid[hexagonCoord.x, hexagonCoord.y];
    }
}
