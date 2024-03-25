using System;
using UnityEngine;
using Utilities.UserInterfaceExtension;

namespace HexagonGrid.GridEditor.Panel
{
    public class BrushButton : AbstractButtonView
    {
        public event Action<HexagonType> BrushChangeHex;

        [SerializeField] private HexagonType hexType;

        protected override void OnClick()
        {
            BrushChangeHex?.Invoke(hexType);
        }
    }
}