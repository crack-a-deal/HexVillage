using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] private HexGridLayout gridLayout;
    [SerializeField] private LineDrawer lineDraw;

    private Hexagon _selectedHexagon;
    private Hexagon _currentHexagon;

    private void Start()
    {
        foreach (var hexagon in gridLayout.Grid)
        {
            hexagon.Selected += SelectStartHexagon;
            hexagon.SelectedCurrent += SelectCurrentHexagon;
        }
    }

    private void OnDestroy()
    {
        foreach (var hexagon in gridLayout.Grid)
        {
            hexagon.Selected -= SelectStartHexagon;
            hexagon.SelectedCurrent -= SelectCurrentHexagon;
        }
    }

    private void SelectCurrentHexagon(Hexagon hexagon)
    {
        if (_selectedHexagon == null)
            return;

        if (_selectedHexagon == hexagon)
        {
            lineDraw.DrawLine(_selectedHexagon, _currentHexagon, gridLayout.Grid, gridLayout.Grid[0, 0].BaseColor);
            return;
        }

        DrawLine(hexagon);
    }


    private void SelectStartHexagon(Hexagon hexagon)
    {
        if (_selectedHexagon == hexagon)
        {
            _selectedHexagon = null;
        }
        else
        {
            _selectedHexagon = hexagon;
            _currentHexagon = null;
        }
    }

    private void DrawLine(Hexagon hexagon)
    {
        if (_currentHexagon != null)
            lineDraw.DrawLine(_selectedHexagon, _currentHexagon, gridLayout.Grid, gridLayout.Grid[0, 0].BaseColor);

        lineDraw.DrawLine(_selectedHexagon, hexagon, gridLayout.Grid, lineDraw.Color);
        _currentHexagon = hexagon;
    }

    private void SelectAllNeighbors(Hexagon targetHexagon)
    {
        if (_selectedHexagon == targetHexagon)
        {
            _selectedHexagon = null;
        }
        else
        {
            _selectedHexagon = targetHexagon;
        }

        //List<Hexagon> list = gridLayout.GetNeighborsList(new Vector2Int(targetHexagon.Column, targetHexagon.Row));
        //foreach (var item in list)
        //{
        //    if (item != null)
        //        item.SetSelectionColor();
        //}
    }
}
