using Pathfinding.BasePathfinding;
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
    public HexGridLayout gridLayout;

    public Hexagon[,] Grid => _grid;
    private Hexagon[,] _grid;


    public HexNode[,] HexGrid => _hexGrid;
    private HexNode[,] _hexGrid;

    private void Awake()
    {
        gridLayout = new HexGridLayout(columnCount, rowCount);
        _hexGrid = new HexNode[columnCount, rowCount];
        gridLayout.CreateLayoutGrid();
        RenderGrid();
    }

    private void RenderGrid()
    {
        _grid = new Hexagon[columnCount, rowCount];

        foreach (Hex hex in gridLayout.Grid)
        {
            Vector2Int offsetCoordination = CoordinateConversion.CubeToOffset(new Vector3Int(hex.Q, hex.R, hex.S));
            Hexagon hexagon = hexagonRenderer.InitHex(offsetCoordination);
            hexagon.HexData = hex;
            hexagon.transform.position = GetHexagonPositionFromCoordinates(offsetCoordination) + (Vector2)transform.position;
            _grid[offsetCoordination.x, offsetCoordination.y] = hexagon;
            _hexGrid[offsetCoordination.x, offsetCoordination.y] = new HexNode(this, _grid[offsetCoordination.x, offsetCoordination.y], offsetCoordination);
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
        foreach (Hexagon hex in _grid)
        {
            hexagonRenderer.ClearToBaseColor(hex);
        }
        UpdateGridLayout?.Invoke();
    }

    public void ClearToDefault()
    {
        foreach (Hexagon hex in _grid)
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
            ChangeTempHexColor(_grid[hex.x, hex.y], color);
        }
    }
    public void DrawBaseLine(List<Vector2Int> path, Color color)
    {
        foreach (var hex in path)
        {
            ChangeBaseHexColor(_grid[hex.x, hex.y], color);
        }
    }

    //public List<Hexagon> GetNeighborsList(Hexagon hexagon)
    //{
    //    List<Hexagon> result = new List<Hexagon>(6);

    //    if (hexagon.Coordinate.x % 2 == 0)
    //    {
    //        // even cols
    //        GetEvenNeigbors(hexagon.Coordinate, result);
    //    }
    //    else
    //    {
    //        // odd cols
    //        GetOddNeigbors(hexagon.Coordinate, result);
    //    }
    //    result.RemoveAll(item => item == null);

    //    return result;
    //}

    //private void GetEvenNeigbors(Vector2Int hexagonCoord, List<Hexagon> neighors)
    //{
    //    neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x + 1, hexagonCoord.y)));
    //    neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x + 1, hexagonCoord.y - 1)));
    //    neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x, hexagonCoord.y - 1)));
    //    neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x - 1, hexagonCoord.y - 1)));
    //    neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x - 1, hexagonCoord.y)));
    //    neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x, hexagonCoord.y + 1)));
    //}

    //private void GetOddNeigbors(Vector2Int hexagonCoord, List<Hexagon> neighors)
    //{
    //    neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x + 1, hexagonCoord.y + 1)));
    //    neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x + 1, hexagonCoord.y)));
    //    neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x, hexagonCoord.y - 1)));
    //    neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x - 1, hexagonCoord.y)));
    //    neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x - 1, hexagonCoord.y + 1)));
    //    neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x, hexagonCoord.y + 1)));
    //}

    //private Hexagon GetNeighbor(Vector2Int hexagonCoord)
    //{
    //    if (hexagonCoord.x < 0 || hexagonCoord.x >= columnCount)
    //        return null;
    //    if (hexagonCoord.y < 0 || hexagonCoord.y >= rowCount)
    //        return null;

    //    return _grid[hexagonCoord.x, hexagonCoord.y];
    //}

    //public List<Node<Vector2Int>> GetNeighborsList(HexNode hexNode)
    //{
    //    List<Node<Vector2Int>> result = new List<Node<Vector2Int>>(6);

    //    if (hexNode.hexagon.Coordinate.x % 2 == 0)
    //    {
    //        // even cols
    //        GetEvenNeigbors(hexNode.hexagon.Coordinate, result);
    //    }
    //    else
    //    {
    //        // odd cols
    //        GetOddNeigbors(hexNode.hexagon.Coordinate, result);
    //    }
    //    result.RemoveAll(item => item == null);

    //    return result;
    //}

    public List<BaseNode<Vector2Int>> GetNeighborsList(HexNode hexNode)
    {
        List<BaseNode<Vector2Int>> result = new List<BaseNode<Vector2Int>>(6);

        if (hexNode.hexagon.Coordinate.x % 2 == 0)
        {
            // even cols
            GetEvenNeigbors(hexNode.hexagon.Coordinate, result);
        }
        else
        {
            // odd cols
            GetOddNeigbors(hexNode.hexagon.Coordinate, result);
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
            return null;
        if (hexagonCoord.y < 0 || hexagonCoord.y >= rowCount)
            return null;

        return _hexGrid[hexagonCoord.x, hexagonCoord.y];
    }
}
