using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UserInterfaceExtension
{
    [RequireComponent(typeof(Toggle))]
    public abstract class AbstractToggleView : MonoBehaviour
    {
        protected Toggle _toggle;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _toggle.onValueChanged.RemoveListener(OnClick);
        }

        protected abstract void OnClick(bool isOn);
    }
}