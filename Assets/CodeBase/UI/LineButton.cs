using System;
using UnityEngine;
using UnityEngine.UI;

public class LineButton : MonoBehaviour
{
    public event Action OnLineClick;
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
        OnLineClick?.Invoke();
    }
}
