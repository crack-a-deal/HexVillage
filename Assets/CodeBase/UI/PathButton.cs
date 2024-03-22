using System;
using UnityEngine;
using UnityEngine.UI;

public class PathButton : MonoBehaviour
{
    public event Action OnPathClick;
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
        OnPathClick?.Invoke();
    }
}
