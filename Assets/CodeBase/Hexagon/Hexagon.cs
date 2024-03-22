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

    public float MovementCost { get; private set; }
    public bool IsWalkable { get; private set; }

    public Hex HexData { get; set; }
    public Vector2Int Coordinate => CoordinateConversion.CubeToOffset(new Vector3Int(HexData.Q, HexData.R, HexData.S));

    private Color _defaultColor;
    public Color DefaultColor
    {
        get => _defaultColor;
        set
        {
            _defaultColor = value;
            BaseColor = _defaultColor;
            TempColor = _defaultColor;
        }
    }
    public Color BaseColor { get; set; }
    public Color TempColor { get; set; }

    #region Pointers
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
    #endregion

    private void ActiveOutline(bool isActive)
    {
        outline.SetActive(isActive);
    }

    public void SetTempColor(Color color)
    {
        TempColor = color;
        hexagonRenderer.color = color;
    }

    public void SetBaseColor(Color color)
    {
        BaseColor = color;
        hexagonRenderer.color = color;
    }
}
