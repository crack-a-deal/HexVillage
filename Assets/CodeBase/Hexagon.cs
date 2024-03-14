using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hexagon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public event Action<Hexagon> Selected;

    [SerializeField] private GameObject outline;
    [SerializeField] private SpriteRenderer hexagonRenderer;
    [SerializeField] private Color selectedColor;

    public int Column { get; set; }
    public int Row { get; set; }

    private Color _baseColor;

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
        if (hexagonRenderer.color == _baseColor)
            hexagonRenderer.color = selectedColor;
        else
            hexagonRenderer.color = _baseColor;
    }

    private void SelectHexagon()
    {
        SetSelectionColor();
        Selected?.Invoke(this);
    }
}
