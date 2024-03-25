using UnityEngine;
using UnityEngine.UI;

namespace HexagonGrid.GridEditor.Panel
{
    public class Brush : MonoBehaviour
    {
        [SerializeField] private Image colorSelector;
        [SerializeField] private HexGridEditor drawer;

        private void Awake()
        {
            drawer.OnCurrentColorChanged += Drawer_OnCurrentColorChanged;
        }

        private void OnDestroy()
        {
            drawer.OnCurrentColorChanged -= Drawer_OnCurrentColorChanged;
        }

        private void Drawer_OnCurrentColorChanged(Color color)
        {
            colorSelector.color = color;
        }
    }
}