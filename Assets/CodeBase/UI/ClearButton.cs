using System;
using UnityEngine;
using UnityEngine.UI;

public class ClearButton : MonoBehaviour
{
    public event Action OnClearClick;
    [SerializeField] private Button button;

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
        OnClearClick?.Invoke();
    }
}
