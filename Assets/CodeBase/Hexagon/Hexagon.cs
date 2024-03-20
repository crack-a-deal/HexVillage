using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hexagon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public event Action<Hexagon> Select;
    public event Action<Hexagon> PointerEnter;
    public event Action<Hexagon> PointerExit;

    [SerializeField] private GameObject outline;
    [SerializeField] private SpriteRenderer hexagonRenderer;

    public Hex HexData { get; set; }
    public Vector2Int Coordinate => CoordinateConversion.CubeToOffset(new Vector3Int(HexData.Q, HexData.R, HexData.S));
    public Color BaseColor { get; set; }

    public Color SelectionColor { get; set; }
    public Color CurrentColor { get; set; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ActiveOutline(true);
        PointerEnter?.Invoke(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Select?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ActiveOutline(false);
        PointerExit?.Invoke(this);
    }

    private void ActiveOutline(bool isActive)
    {
        outline.SetActive(isActive);
    }

    public void SetSelectionColor()
    {
        ChangeColor(SelectionColor);
    }

    public void SetBaseColor()
    {
        ChangeColor(BaseColor);
    }

    public void ChangeColor(Color color)
    {
        CurrentColor = color;
        hexagonRenderer.color = color;
        return;
    }
}
