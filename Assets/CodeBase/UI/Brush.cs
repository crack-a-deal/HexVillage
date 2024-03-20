using UnityEngine;
using UnityEngine.UI;

public class Brush : MonoBehaviour
{
    [SerializeField] private Image colorSelector;
    [SerializeField] private HexGridDrawer drawer;

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
