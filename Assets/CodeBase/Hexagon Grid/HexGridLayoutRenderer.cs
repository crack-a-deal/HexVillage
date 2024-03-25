using System;
using System.Collections.Generic;
using UnityEngine;

namespace HexagonGrid
{
    [RequireComponent(typeof(HexagonRenderer))]
    public class HexGridLayoutRenderer : MonoBehaviour
    {
        public event Action UpdateGridLayout;

        [Header("Grid Size")]
        [SerializeField] private int columnCount;
        [SerializeField] private int rowCount;

        [Space]
        [SerializeField] private HexagonRenderer hexagonRenderer;
        public HexGridLayout GridLayout { get; private set; } = new HexGridLayout();

        public Hexagon[,] Grid => _hexagonsGrid;
        private Hexagon[,] _hexagonsGrid;

        private void Awake()
        {
            GridLayout.CreateLayoutGrid(columnCount, rowCount);
            RenderGrid();
        }

        private void RenderGrid()
        {
            _hexagonsGrid = new Hexagon[columnCount, rowCount];

            foreach (Hex hex in GridLayout.Grid)
            {
                Vector2Int offsetCoordination = CoordinateConversion.CubeToOffset(new Vector3Int(hex.Q, hex.R, hex.S));
                Hexagon hexagon = hexagonRenderer.CreateHex(offsetCoordination);
                hexagon.HexData = hex;
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

        public Hexagon GetHexagon(Vector2Int coordinates)
        {
            return Grid[coordinates.x, coordinates.y];
        }

        public bool CheckHexagon(Vector2Int coordinates)
        {
            return Grid[coordinates.x,coordinates.y].IsWalkable;
        }
            

        #region Render Methods
        public void ClearFrame()
        {
            foreach (Hexagon hex in _hexagonsGrid)
            {
                hexagonRenderer.ClearToBaseColor(hex);
            }
            UpdateGridLayout?.Invoke();
        }

        public void ClearToDefault()
        {
            foreach (Hexagon hex in _hexagonsGrid)
            {
                hexagonRenderer.ClearToDefaultColor(hex);
            }
            UpdateGridLayout?.Invoke();
        }

        public void ChangeTempHexColor(Hexagon hex, Color color)
        {
            hexagonRenderer.ChangeTempHexagonColor(hex, color);
            UpdateGridLayout?.Invoke();
        }

        public void ChangeBaseHexColor(Hexagon hex, Color color)
        {
            hexagonRenderer.ChangeBaseHexagonColor(hex, color);
            UpdateGridLayout?.Invoke();
        }

        public Color GetColor(HexagonType type) => hexagonRenderer.GetColorFromType(type);

        public void DrawLine(List<Vector2Int> path, Color color)
        {
            ClearFrame();
            foreach (var hex in path)
            {
                // TODO: fix IndexOutOfRangeException
                ChangeTempHexColor(_hexagonsGrid[hex.x, hex.y], color);
            }
        }
        public void DrawBaseLine(List<Vector2Int> path, Color color)
        {
            foreach (var hex in path)
            {
                ChangeBaseHexColor(_hexagonsGrid[hex.x, hex.y], color);
            }
        }
        #endregion
    }
}