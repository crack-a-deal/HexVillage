using System;
using Utilities.UserInterfaceExtension;

namespace HexagonGrid.GridEditor.Panel
{
    public class ClearButton : AbstractButtonView
    {
        public event Action OnClearClick;

        protected override void OnClick()
        {
            OnClearClick?.Invoke();

        }
    }
}