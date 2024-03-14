using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] private HexGridLayout gridLayout;
    [SerializeField] private LineDraw lineDraw;

    private Hexagon selectedHexagon;
    private Hexagon tempObj;

    private void Start()
    {
        foreach (var item in gridLayout.Grid)
        {
            item.SelectedCurrent += DrawLine;
            item.Selected += SelectAllNeighbors;
        }
    }

    private void DrawLine(Hexagon obj)
    {
        if (selectedHexagon == null)
            return;

        if (selectedHexagon == obj)
            return;

        if (tempObj != null)
            lineDraw.DrawLine(selectedHexagon, tempObj, gridLayout.Grid, gridLayout.Grid[0, 0].BaseColor);

        lineDraw.DrawLine(selectedHexagon, obj, gridLayout.Grid, lineDraw.Color);
        tempObj = obj;
    }

    private void SelectAllNeighbors(Hexagon targetHexagon)
    {
        if (selectedHexagon == targetHexagon)
        {
            selectedHexagon = null;
        }
        else
        {
            selectedHexagon = targetHexagon;
        }

        //List<Hexagon> list = gridLayout.GetNeighborsList(new Vector2Int(targetHexagon.Column, targetHexagon.Row));
        //foreach (var item in list)
        //{
        //    if (item != null)
        //        item.SetSelectionColor();
        //}
    }
}
