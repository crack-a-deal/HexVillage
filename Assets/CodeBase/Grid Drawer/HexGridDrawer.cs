using System;
using UnityEngine;

public class HexGridDrawer : MonoBehaviour
{
    public event Action<Color> OnCurrentColorChanged;

    // TODO: line drawer
    // TODO: start point drawer
    // TODO: end point drawer

    [SerializeField] private HexGridLayoutRenderer gridRenderer;
    [SerializeField] private LineDrawer lineDrawer;
    [SerializeField] private DrawerToolsPanel toolsPanel;


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
    }

    private void Start()
    {
        foreach (Hexagon hex in gridRenderer.Grid)
        {
            hex.Select += Hex_Select;
        }
    }

    private void Hex_Select(Hexagon hex)
    {
        _selectedHexagon = hex;
        gridRenderer.ChangeHexColor(hex,CurrentColor);
    }

    private void ToolsPanel_ClearField()
    {
        gridRenderer.Clear();
    }

    private void ToolsPanel_ChangeHexagonType(HexagonType type)
    {
        HexagonType = type;
    }
}
