using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hexagon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public event Action<Hexagon> Selected;
    public event Action<Hexagon> SelectedCurrent;

    [SerializeField] private GameObject outline;
    [SerializeField] private SpriteRenderer hexagonRenderer;

    public int Column { get; set; }

    public int Row { get; set; }

    public Vector2Int Coordinate { get => new Vector2Int(Column, Row); set { Column = value.x; Row = value.y; } }

    public Color BaseColor { get; set; }

    public Color SelectionColor { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        SelectHexagon();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ActiveOutline(true);
        SelectedCurrent?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ActiveOutline(false);
    }

    private void ActiveOutline(bool isActive)
    {
        outline.SetActive(isActive);
    }

    public void SetSelectionColor()
    {
        SetSelectedColor(SelectionColor);
    }

    public void SetBaseColor()
    {
        SetSelectedColor(BaseColor);
    }

    public void SetSelectedColor(Color color)
    {
        hexagonRenderer.color = color;
        return;
    }

    private void SelectHexagon()
    {
        SetSelectionColor();
        Selected?.Invoke(this);
    }
}
