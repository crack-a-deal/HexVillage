using System;
using Utilities.UserInterfaceExtension;

namespace HexagonGrid.GridEditor.Panel
{
    public class PathButton : AbstractButtonView
    {
        public event Action OnPathClick;

        protected override void OnClick()
        {
            OnPathClick?.Invoke();
        }
    }
}