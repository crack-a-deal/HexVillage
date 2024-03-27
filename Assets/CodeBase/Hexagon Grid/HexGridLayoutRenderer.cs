using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexagonGrid
{
    public class HexGridLayoutRenderer : MonoBehaviour
    {
        public event Action UpdateGridLayout;

        [Header("Grid Size")]
        [SerializeField] private int columnCount;
        [SerializeField] private int rowCount;

        [Space]
        [Header("Hexagon Setting")]
        [SerializeField] private Hexagon hexagonPrefab;
        [SerializeField] private HexagonData[] hexagonTypes;

        public HexGridLayout GridLayout { get; private set; } = new HexGridLayout();

        public Hexagon[,] Grid => _hexagonsGrid;
        private Hexagon[,] _hexagonsGrid;

        private void Awake()
        {
            GridLayout.CreateLayoutGrid(columnCount, rowCount);
            RenderGrid();
        }

        #region Grid
        private void RenderGrid()
        {
            _hexagonsGrid = new Hexagon[columnCount, rowCount];

            foreach (Hex hex in GridLayout.Grid)
            {
                Vector2Int offsetCoordination = CoordinateConversion.CubeToOffset(new Vector3Int(hex.Q, hex.R, hex.S));

                Hexagon hexagon = CreateHexagon(offsetCoordination, hex);
                hexagon.transform.position = GetHexagonPositionFromCoordinates(offsetCoordination) + (Vector2)transform.position;
                _hexagonsGrid[offsetCoordination.x, offsetCoordination.y] = hexagon;
            }
            UpdateGridLayout?.Invoke();
        }

        private Vector2 GetHexagonPositionFromCoordinates(Vector2Int coordinates)
        {
            int column = coordinates.x;
            int row = coordinates.y;

            bool shoutOffset = column % 2 != 0;
            float offset = shoutOffset ? 0.45f : 0f;

            float rowPosition = row + offset - 0.1f * row;
            float colPosition = column - column * 0.2f;
            rowPosition *= /*isEvenColum ? 1 :*/ -1;
            return new Vector2(colPosition, rowPosition);
        }
        #endregion

        #region Hexagon
        private Hexagon CreateHexagon(Vector2Int coord, Hex hex)
        {
            Hexagon hexagon = Instantiate(hexagonPrefab, transform);
            hexagon.name = $"Hexagon [{coord.x},{coord.y}]";

            hexagon.DefaultColor = hexagonTypes[0].Color;
            hexagon.IsWalkable = hexagonTypes[0].IsWalkable;
            hexagon.HexagonData = hexagonTypes[0];

            hexagon.HexData = hex;

            return hexagon;
        }

        public Hexagon GetHexagon(Vector2Int coordinates)
        {
            return Grid[coordinates.x, coordinates.y];
        }

        public bool CheckHexagon(Vector2Int coordinates)
        {
            return Grid[coordinates.x, coordinates.y].IsWalkable;
        }

        private void SetColor(Hexagon hex, Color color)
        {
            hex.BaseColor = color;
            hex.HexagonData = GetHexagonData(color);
            hex.IsWalkable = hex.HexagonData.IsWalkable;
        }

        public HexagonData GetHexagonData(Color color)
        {
            return hexagonTypes.FirstOrDefault(x => x.Color == color);
        }

        public Color GetColor(HexagonType type)
        {
            return hexagonTypes.FirstOrDefault(x => x.HexagonType == type).Color;
        }

        public HexagonType GetType(Color color)
        {
            return hexagonTypes.FirstOrDefault(x => x.Color == color).HexagonType;
        }
        #endregion

        #region Render Methods
        public void Draw(List<Vector2Int> list, Color color)
        {
            foreach (Vector2Int hex in list)
            {
                SetColor(_hexagonsGrid[hex.x, hex.y], color);
            }
            UpdateGridLayout?.Invoke();
        }

        public void Draw(Color color)
        {
            foreach (Hexagon item in _hexagonsGrid)
            {
                SetColor(item, color);
            }
            UpdateGridLayout?.Invoke();
        }

        public void Draw(Hexagon hex, Color color)
        {
            SetColor(hex, color);
            UpdateGridLayout?.Invoke();
        }

        public void Clear(List<Vector2Int> list, Color color)
        {
            foreach (Vector2Int hex in list)
            {
                SetColor(_hexagonsGrid[hex.x, hex.y], _hexagonsGrid[hex.x, hex.y].DefaultColor);
            }
            UpdateGridLayout?.Invoke();
        }

        public void Clear(Color color)
        {
            foreach (Hexagon item in _hexagonsGrid)
            {
                SetColor(item, color);
            }
            UpdateGridLayout?.Invoke();
        }
        #endregion
    }
}