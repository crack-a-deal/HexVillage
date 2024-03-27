using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HexagonGrid
{
    public class Hexagon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public event Action<Hexagon> Select;
        public event Action<Hexagon> PointerEnter;
        public event Action<Hexagon> PointerExit;

        [SerializeField] private GameObject outline;
        [SerializeField] private SpriteRenderer hexagonRenderer;

        public bool IsWalkable { get; set; }

        public HexagonData HexagonData { get; set; }
        public Hex HexData { get; set; }
        public Vector2Int Coordinate => CoordinateConversion.CubeToOffset(new Vector3Int(HexData.Q, HexData.R, HexData.S));

        //TODO: private set
        public Color DefaultColor { get; set; }

        private Color _baseColor;
        public Color BaseColor
        {
            get => _baseColor;
            set
            {
                _baseColor = value;
                hexagonRenderer.color = _baseColor;
            }
        }

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
    }
}