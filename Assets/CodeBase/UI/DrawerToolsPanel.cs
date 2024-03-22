using System;
using UnityEngine;

public class DrawerToolsPanel : MonoBehaviour
{
    public event Action ClearField;
    public event Action DrawLine;
    public event Action FindPath;


    public event Action<HexagonType> ChangeHexagonType;

    [SerializeField] private ClearButton clearButton;
    [SerializeField] private BrushButton[] brushButtons;
    [SerializeField] private LineButton lineButton;
    [SerializeField] private PathButton pathButton;

    private void Awake()
    {
        clearButton.OnClearClick += ClearButton_OnClearClick;
        lineButton.OnLineClick += LineButton_OnLineClick;
        pathButton.OnPathClick += PathButton_OnPathClick;

        foreach (BrushButton brush in brushButtons)
        {
            brush.BrushChangeHex += Brush_BrushChangeHex;
        }
    }

    private void OnDestroy()
    {
        clearButton.OnClearClick -= ClearButton_OnClearClick;
        lineButton.OnLineClick -= LineButton_OnLineClick;
        pathButton.OnPathClick -= PathButton_OnPathClick;

        foreach (BrushButton brush in brushButtons)
        {
            brush.BrushChangeHex -= Brush_BrushChangeHex;
        }
    }

    private void ClearButton_OnClearClick()
    {
        ClearField?.Invoke();
    }

    private void LineButton_OnLineClick()
    {
        DrawLine?.Invoke();
    }

    private void PathButton_OnPathClick()
    {
        FindPath?.Invoke();
    }

    private void Brush_BrushChangeHex(HexagonType type)
    {
        ChangeHexagonType?.Invoke(type);
    }
}
