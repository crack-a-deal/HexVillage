using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hexagon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject outline;

    public int Column { get; set; }
    public int Row { get; set; }

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
}
