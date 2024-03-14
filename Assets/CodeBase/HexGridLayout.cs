using System.Collections.Generic;
using UnityEngine;

public class HexGridLayout : MonoBehaviour
{
    [SerializeField] private int columnCount;
    [SerializeField] private int rowCount;
    [SerializeField] private Hexagon hexagonPrefab;

    [SerializeField] private bool isEvenColum = true;
    [SerializeField] private Color evenColor;

    [SerializeField] private Hexagon[,] hexagonGrid;

    public Hexagon[,] Grid => hexagonGrid;

    private void Awake()
    {
        hexagonGrid = new Hexagon[columnCount, rowCount];
        LayoutGrid();
    }

    private void LayoutGrid()
    {
        for (int r = 0; r < rowCount; r++)
        {
            for (int q = 0; q < columnCount; q++)
            {
                Hexagon hexagon = Instantiate(hexagonPrefab, transform);
                hexagon.name = $"Hexagon [{q},{r}]";
                hexagon.transform.position = GetHexagonPositionFromCoordinates(new Vector2Int(q, r)) + (Vector2)transform.position;

                hexagon.Column = q;
                hexagon.Row = r;
                hexagonGrid[q, r] = hexagon;
            }
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
        rowPosition *= isEvenColum ? 1 : -1;
        return new Vector2(colPosition, rowPosition);
    }
}
