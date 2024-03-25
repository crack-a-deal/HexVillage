using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexagonGrid
{
    public class HexagonRenderer : MonoBehaviour
    {
        [Header("Hexagon Setting")]
        [SerializeField] private Hexagon hexagonPrefab;
        [SerializeField] private HexagonData[] hexagonTypes;

        private Dictionary<HexagonType, Color> hexagonTypesDictionary = new Dictionary<HexagonType, Color>();

        private void Awake()
        {
            hexagonTypesDictionary = hexagonTypes.ToDictionary(type => type.HexagonType, type => type.Color);
        }

        public Hexagon CreateHex(Vector2Int coord)
        {
            Hexagon hexagon = Instantiate(hexagonPrefab, transform);
            hexagon.name = $"Hexagon [{coord.x},{coord.y}]";

            hexagon.DefaultColor = hexagonTypes[0].Color;
            hexagon.IsWalkable = hexagonTypes[0].IsWalkable;
            hexagon.HexagonData = hexagonTypes[0];
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
            hex.HexagonData = hexagonTypes.FirstOrDefault(x => x.Color == baseColor);
            hex.IsWalkable = hex.HexagonData.IsWalkable;
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
}