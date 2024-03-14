using System.Collections.Generic;
using UnityEngine;

public class HexGridLayout : MonoBehaviour
{
    [SerializeField] private int columnCount;
    [SerializeField] private int rowCount;

    [SerializeField] private bool isEvenColum = true;

    [Header("Hexagon Setting")]
    [SerializeField] private Hexagon hexagonPrefab;
    [SerializeField] private Color hexagonBaseColor;
    [SerializeField] private Color hexagonSelectionColor;

    private Hexagon[,] hexagonGrid;

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

                hexagon.BaseColor = hexagonBaseColor;
                hexagon.SelectionColor = hexagonSelectionColor;
                hexagon.SetBaseColor();

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

    public List<Hexagon> GetNeighborsList(Vector2Int hexagonCoord)
    {
        List<Hexagon> neighors = new List<Hexagon>(6);

        if (hexagonCoord.x % 2 == 0)
        {
            // even cols
            GetEvenNeigbors(hexagonCoord, neighors);
        }
        else
        {
            // odd cols
            GetOddNeigbors(hexagonCoord, neighors);
        }

        return neighors;
    }

    private void GetEvenNeigbors(Vector2Int hexagonCoord, List<Hexagon> neighors)
    {
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x + 1, hexagonCoord.y)));
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x + 1, hexagonCoord.y - 1)));
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x, hexagonCoord.y - 1)));
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x - 1, hexagonCoord.y - 1)));
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x - 1, hexagonCoord.y)));
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x, hexagonCoord.y + 1)));
    }

    private void GetOddNeigbors(Vector2Int hexagonCoord, List<Hexagon> neighors)
    {
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x + 1, hexagonCoord.y + 1)));
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x + 1, hexagonCoord.y)));
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x, hexagonCoord.y - 1)));
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x - 1, hexagonCoord.y)));
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x - 1, hexagonCoord.y + 1)));
        neighors.Add(GetNeighbor(new Vector2Int(hexagonCoord.x, hexagonCoord.y + 1)));
    }

    private Hexagon GetNeighbor(Vector2Int hexagonCoord)
    {
        if (hexagonCoord.x < 0 || hexagonCoord.x >= columnCount)
            return null;
        if (hexagonCoord.y < 0 || hexagonCoord.y >= rowCount)
            return null;

        return hexagonGrid[hexagonCoord.x, hexagonCoord.y];
    }
}
