using System;
using UnityEngine;
using UnityEngine.UI;
using Utilities.UserInterfaceExtension;

namespace HexagonGrid.GridEditor.Panel
{
    public class LineButton : AbstractToggleView
    {
        public event Action OnLineClick;

        [SerializeField] private Image background;
        [SerializeField] private Color activeColor;

        protected override void OnClick(bool isOn)
        {
            OnLineClick?.Invoke();
            background.color = isOn ? activeColor : Color.white;
        }
    }
}