using System;
using UnityEngine;

public class HexGridDrawer : MonoBehaviour
{
    public event Action<Color> OnCurrentColorChanged;

    // TODO: start point drawer
    // TODO: end point drawer

    [SerializeField] private HexGridLayoutRenderer gridRenderer;
    [SerializeField] private LineDrawer lineDrawer;
    [SerializeField] private DrawerToolsPanel toolsPanel;

    [SerializeField] private bool isDrawLine = false;
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
            if(_startHex != null)
            {
                _endHex = hex;
                gridRenderer.DrawLine(lineDrawer.GetLinePath(_startHex, _endHex), CurrentColor);
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
            if(_startHex != null && _endHex != null)
            {
                gridRenderer.DrawBaseLine(lineDrawer.GetLinePath(_startHex, _endHex), CurrentColor);
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

    private void ToolsPanel_ChangeHexagonType(HexagonType type)
    {
        HexagonType = type;
    }
}
