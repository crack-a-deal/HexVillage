using UnityEngine;

public class HexGridLayout
{
    public int Columns => _columnCount;
    public int Rows => _rowCount;
    public Hex[,] Grid => _grid;

    private int _columnCount;
    private int _rowCount;
    private Hex[,] _grid;


    public HexGridLayout(int columnCount, int rowCount)
    {
        _columnCount = columnCount;
        _rowCount = rowCount;
    }

    public void CreateLayoutGrid()
    {
        _grid = new Hex[_columnCount, _rowCount];

        for (int r = 0; r < _rowCount; r++)
        {
            for (int q = 0; q < _columnCount; q++)
            {
                Vector3Int cubeCoordinates = CoordinateConversion.OffsetToCube(new Vector2Int(q, r));
                Hex hexq = new Hex(cubeCoordinates.x, cubeCoordinates.y, cubeCoordinates.z);
                _grid[q, r] = hexq;
            }
        }
    }
}
