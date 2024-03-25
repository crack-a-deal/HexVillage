using HexagonGrid.GridEditor.Panel;
using Pathfinding.HexPathfinding;
using System;
using UnityEngine;

namespace HexagonGrid.GridEditor
{
    public class HexGridEditor : MonoBehaviour
    {
        public event Action<Color> OnCurrentColorChanged;

        [SerializeField] private HexGridLayoutRenderer gridRenderer;
        [SerializeField] private LineDrawer lineDrawer;
        [SerializeField] private HexPathfinder pathfinder;
        [SerializeField] private HexGridEditorPanel toolsPanel;

        [SerializeField] private bool isDrawLine = false;
        [SerializeField] private bool isPathfinding = false;

        private Hexagon _startHex;
        private Hexagon _endHex;

        private Hexagon _selectedHexagon;
        public Hexagon SelectedHexagon
        {
            get
            {
                return _selectedHexagon;
            }
            private set
            {
                if (_selectedHexagon != value)
                {
                    _selectedHexagon = value;
                }
            }
        }

        private HexagonType _hexagonType = HexagonType.None;
        public HexagonType HexagonType
        {
            get => _hexagonType;
            set
            {
                if (value != _hexagonType)
                {
                    _hexagonType = value;
                    CurrentColor = gridRenderer.GetColor(_hexagonType);
                }
            }
        }

        private Color _currentColor;
        public Color CurrentColor
        {
            get => _currentColor;
            set
            {
                if (value != _currentColor)
                {
                    _currentColor = value;
                    OnCurrentColorChanged?.Invoke(_currentColor);
                }
            }
        }

        private void Awake()
        {
            toolsPanel.ClearField += ToolsPanel_ClearField;
            toolsPanel.ChangeHexagonType += ToolsPanel_ChangeHexagonType;
            toolsPanel.DrawLine += ToolsPanel_DrawLine;
            toolsPanel.FindPath += ToolsPanel_FindPath;

            gridRenderer.UpdateGridLayout += GridRenderer_UpdateGridLayout;
            pathfinder.PathFounded += Pathfinder_OnPathFound;
        }

        private void Pathfinder_OnPathFound()
        {
            gridRenderer.DrawBaseLine(pathfinder.Path, CurrentColor);
        }

        private void GridRenderer_UpdateGridLayout()
        {
            if (!isPathfinding)
                return;
        }

        private void Start()
        {
            foreach (Hexagon hex in gridRenderer.Grid)
            {
                hex.Select += Hex_Select;
                hex.PointerEnter += Hex_PointerEnter;
            }
        }

        private void Hex_PointerEnter(Hexagon hex)
        {
            if (isDrawLine)
            {
                if (_startHex != null)
                {
                    _endHex = hex;
                    gridRenderer.DrawLine(lineDrawer.GetLinePath(_startHex, _endHex), CurrentColor);
                }
            }
            else if (isPathfinding)
            {
                if (_startHex != null)
                {
                    _endHex = hex;
                }
            }
        }

        private void Hex_Select(Hexagon hex)
        {
            if (isDrawLine)
            {
                if (_startHex == null)
                {
                    _startHex = hex;
                    return;
                }
                if (_startHex != null && _endHex != null)
                {
                    gridRenderer.DrawBaseLine(lineDrawer.GetLinePath(_startHex, _endHex), CurrentColor);
                    _startHex = null;
                    _endHex = null;
                }
            }
            else if (isPathfinding)
            {
                if (_startHex == null)
                {
                    _startHex = hex;
                    return;
                }
                if (_startHex != null && _endHex != null)
                {
                    pathfinder.FindPath(_startHex, _endHex);
                    _startHex = null;
                    _endHex = null;
                }
            }
            else
            {
                _selectedHexagon = hex;
                gridRenderer.ChangeBaseHexColor(hex, CurrentColor);
            }
        }

        private void ToolsPanel_ClearField()
        {
            gridRenderer.ClearToDefault();
        }

        private void ToolsPanel_DrawLine()
        {
            isDrawLine = !isDrawLine;
        }

        private void ToolsPanel_FindPath()
        {
            isPathfinding = !isPathfinding;
        }

        private void ToolsPanel_ChangeHexagonType(HexagonType type)
        {
            HexagonType = type;
        }
    }
}