using UnityEngine;

public class HexGridLayout : MonoBehaviour
{
    private float width = 1, height = 1;
    [SerializeField] private int columnCount;
    [SerializeField] private int rowCount;
    [SerializeField] private GameObject hexagonPrefab;

    private void Start()
    {
        LayoutGrid();
    }

    private void LayoutGrid()
    {
        for (int r = 0; r < rowCount; r++)
        {
            for (int q = 0; q < columnCount; q++)
            {
                GameObject hexagon = Instantiate(hexagonPrefab, transform);
                hexagon.transform.position = GetHexagonPositionFromCoordinates(new Vector2Int(q, r));
            }
        }
    }

    private Vector2 GetHexagonPositionFromCoordinates(Vector2Int coordinates)
    {
        int row = coordinates.y;
        int column = coordinates.x;

        bool shoutOffset = column % 2 != 0;
        float offset = shoutOffset ? width / 2 : 0;

        float horiz = 3 / 4 * width;
        float vert = height;

        float rowPosition = row * vert + offset;
        float colPosition = column;
        Debug.Log($"Hex coordinates Row:{row} Column:{column}\n rPosition:{rowPosition} cPosition:{colPosition}");
        return new Vector2(colPosition, rowPosition);
    }
}
