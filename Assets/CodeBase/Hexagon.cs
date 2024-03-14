using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hexagon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public event Action<Hexagon> Selected;
    public event Action<Hexagon> SelectedCurrent;

    [SerializeField] private GameObject outline;
    [SerializeField] private SpriteRenderer hexagonRenderer;
    [SerializeField] private Color selectedColor;

    public int Column { get; set; }
    public int Row { get; set; }

    private Color _baseColor;

    public Color BaseColor => _baseColor;

    private void Awake()
    {
        _baseColor = hexagonRenderer.color;
    }

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
        SetSelectedColor(selectedColor);
    }

    public void SetBaseColor()
    {
        SetSelectedColor(_baseColor);
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
