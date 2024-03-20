using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexagonRenderer : MonoBehaviour
{
    [Header("Hexagon Setting")]
    [SerializeField] private Hexagon hexagonPrefab;
    [SerializeField] private Color hexagonBaseColor;
    [SerializeField] private Color hexagonSelectionColor;

    [Space]
    [SerializeField] private HexagonColor[] hexagonTypes;
    private Dictionary<HexagonType, Color> hexagonTypesDictionary;

    private void Awake()
    {
        hexagonTypesDictionary = new Dictionary<HexagonType, Color>();
        hexagonTypesDictionary = hexagonTypes.ToDictionary(type => type.HexagonType, type => type.BaseColor);
    }

    public Hexagon InitHex(Vector2Int coord)
    {
        Hexagon hexagon = Instantiate(hexagonPrefab, transform);
        hexagon.name = $"Hexagon [{coord.x},{coord.y}]";
        hexagon.BaseColor = hexagonTypes[0].BaseColor;
        hexagon.SelectionColor = hexagonSelectionColor;
        hexagon.SetBaseColor();
        return hexagon;
    }

    public void ChangeHexagonColor(Hexagon hex, Color color)
    {
        hex.ChangeColor(color);
    }

    public Color GetColorFromType(HexagonType type)
    {
        return hexagonTypesDictionary[type];
    }
}
