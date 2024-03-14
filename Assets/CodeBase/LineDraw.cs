using System.Collections.Generic;
using UnityEngine;

public class LineDraw : MonoBehaviour
{
    [SerializeField] private Color lineColor;

    public Color Color => lineColor;

    public void DrawLine(Hexagon start, Hexagon target, Hexagon[,] hexGrid, Color color)
    {
        var N = Distance.GetOffsetDistance(new Vector2Int(start.Column, start.Row), new Vector2Int(target.Column, target.Row));
        List<Vector3Int> position = GetHexagonCoord(start, target, N);

        foreach (var item in position)
        {
            Vector2Int coord = CoordinateConversion.CubeToOffset(item);
            hexGrid[coord.x, coord.y].SetSelectedColor(color);
        }
    }

    private List<Vector3Int> GetHexagonCoord(Hexagon start, Hexagon target, int N)
    {
        List<Vector3Int> result = new List<Vector3Int>();
        Vector3Int sC = CoordinateConversion.AxialToCube(CoordinateConversion.OffsetToAxial(new Vector2Int(start.Column, start.Row)));
        Vector3Int tC = CoordinateConversion.AxialToCube(CoordinateConversion.OffsetToAxial(new Vector2Int(target.Column, target.Row)));

        for (int i = 0; i <= N; i++)
        {
            result.Add(CubeRound(CubeLerp(sC, tC, 1.0f / N * i)));
        }
        return result;
    }

    private Vector3Int CubeRound(Vector3 flatPoint)
    {
        int q = Mathf.RoundToInt(flatPoint.x);
        int r = Mathf.RoundToInt(flatPoint.y);
        int s = Mathf.RoundToInt(flatPoint.z);

        var q_diff = Mathf.Abs(q - flatPoint.x);
        var r_diff = Mathf.Abs(r - flatPoint.y);
        var s_diff = Mathf.Abs(s - flatPoint.z);

        if (q_diff > r_diff && q_diff > s_diff)
            q = -r - s;
        else if (r_diff > s_diff)
            r = -q - s;
        else
            s = -q - r;
        return new Vector3Int(q, r, s);
    }

    private Vector3 CubeLerp(Vector3Int f, Vector3Int s, float t)
    {
        Vector3 result = new Vector3(
            Mathf.Lerp(f.x, s.x, t),
            Mathf.Lerp(f.y, s.y, t),
            Mathf.Lerp(f.z, s.z, t)
            );
        return result;
    }

    private float Lerp(float a, float b, float t)
    {
        return a + (b - a) * t;
    }
}
