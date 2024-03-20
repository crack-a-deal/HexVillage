using System;
using UnityEngine;
using UnityEngine.UI;

public class BrushButton : MonoBehaviour
{
    public event Action<HexagonType> BrushChangeHex;
    [SerializeField] private Button button;
    [SerializeField] private HexagonType hexType;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        BrushChangeHex?.Invoke(hexType);
    }
}
