using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HexagonRenderer))]
public class HexGridLayoutRenderer : MonoBehaviour
{
    [Header("Grid Size")]
    [SerializeField] private int columnCount;
    [SerializeField] private int rowCount;

    [Space]
    [SerializeField] private HexagonRenderer hexagonRenderer;
    private HexGridLayout gridLayout;

    public Hexagon[,] Grid => _grid;
    private Hexagon[,] _grid;

    private void Awake()
    {
        gridLayout = new HexGridLayout(columnCount, rowCount);
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
        }
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
    }

    public void ClearToDefault()
    {
        foreach (Hexagon hex in _grid)
        {
            hexagonRenderer.ClearToDefaultColor(hex);
        }
    }

    public void ChangeTempHexColor(Hexagon hex, Color color)
    {
        hexagonRenderer.ChangeTempHexagonColor(hex, color);
    }

    public void ChangeBaseHexColor(Hexagon hex,Color color)
    {
        hexagonRenderer.ChangeBaseHexagonColor(hex, color);
    }

    public Color GetColor(HexagonType type) => hexagonRenderer.GetColorFromType(type);

    public void DrawLine(List<Vector2Int> path, Color color)
    {
        ClearFrame();
        foreach (var hex in path)
        {
            ChangeTempHexColor(_grid[hex.x,hex.y], color);
        }
    }
    public void DrawBaseLine(List<Vector2Int> path, Color color)
    {
        foreach (var hex in path)
        {
            ChangeBaseHexColor(_grid[hex.x, hex.y], color);
        }
    }
}
