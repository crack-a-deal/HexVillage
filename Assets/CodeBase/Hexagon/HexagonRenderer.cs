using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexagonRenderer : MonoBehaviour
{
    [Header("Hexagon Setting")]
    [SerializeField] private Hexagon hexagonPrefab;
    [SerializeField] private Color hexagonBaseColor;

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

        hexagon.DefaultColor = hexagonTypes[0].BaseColor;
        hexagon.SetTempColor(hexagon.DefaultColor);
        return hexagon;
    }

    public void ChangeTempHexagonColor(Hexagon hex, Color color)
    {
        hex.SetTempColor(color);
    }

    public void ChangeBaseHexagonColor(Hexagon hex, Color baseColor)
    {
        hex.SetBaseColor(baseColor);
    }

    public void ClearToDefaultColor(Hexagon hex)
    {
        hex.SetBaseColor(hex.DefaultColor);
    }

    public void ClearToBaseColor(Hexagon hex)
    {
        hex.SetTempColor(hex.BaseColor);
    }

    public Color GetColorFromType(HexagonType type)
    {
        return hexagonTypesDictionary[type];
    }
}
