using System;
using Utilities.UserInterfaceExtension;

namespace HexagonGrid.GridEditor.Panel
{
    public class LineButton : AbstractButtonView
    {
        public event Action OnLineClick;

        protected override void OnClick()
        {
            OnLineClick?.Invoke();
        }
    }
}