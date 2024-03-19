using UnityEngine;

public class HexagonRenderer : MonoBehaviour
{
    [Header("Hexagon Setting")]
    [SerializeField] private Hexagon hexagonPrefab;
    [SerializeField] private Color hexagonBaseColor;
    [SerializeField] private Color hexagonSelectionColor;

    [Space]
    [SerializeField] private HexagonColor[] hexagonTypes;

    public Hexagon InitHex(Vector2Int coord)
    {
        Hexagon hexagon = Instantiate(hexagonPrefab, transform);
        hexagon.name = $"Hexagon [{coord.x},{coord.y}]";

        hexagon.Coordinate = coord;
        int type = GetHexType();
        hexagon.BaseColor = hexagonTypes[type].BaseColor;
        hexagon.SelectionColor = hexagonSelectionColor;
        hexagon.SetBaseColor();
        return hexagon;
    }

    private int GetHexType()
    {
        int typyIndex = Random.Range(0, hexagonTypes.Length);
        return typyIndex;
    }
}

[System.Serializable]
public class HexagonColor
{
    public HexagonType HexagonType;
    public Color BaseColor;
    public Color SelectionColor;
}
