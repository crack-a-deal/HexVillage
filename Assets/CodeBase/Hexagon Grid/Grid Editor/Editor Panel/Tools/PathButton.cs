using System;
using UnityEngine;
using UnityEngine.UI;
using Utilities.UserInterfaceExtension;

namespace HexagonGrid.GridEditor.Panel
{
    public class PathButton : AbstractToggleView
    {
        public event Action OnPathClick;

        [SerializeField] private Image background;
        [SerializeField] private Color activeColor;

        protected override void OnClick(bool isOn)
        {
            OnPathClick?.Invoke();
            background.color = isOn ? activeColor : Color.white;
        }
    }
}