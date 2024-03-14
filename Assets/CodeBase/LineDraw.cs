using System;
using System.Collections.Generic;
using UnityEngine;

public class LineDraw : MonoBehaviour
{
    [SerializeField] private Color lineColor;

    public Color Color => lineColor;

    public void DrawLine(Hexagon start, Hexagon target, Hexagon[,] hexGrid, Color color)
    {
        int N = Distance.GetOffsetDistance(new Vector2Int(start.Column, start.Row), new Vector2Int(target.Column, target.Row));
        List<Vector3Int> position = GetHexagonCoord(start, target, N);
        Debug.Log("---");
        foreach (var item in position)
        {
            Vector2Int coord = CoordinateConversion.CubeToOffset(item);
            Debug.Log($"{item.x} {item.y} {item.z} || {coord.x} {coord.y}");
            hexGrid[coord.x, coord.y].SetSelectedColor(color);
        }
    }

    private List<Vector3Int> GetHexagonCoord(Hexagon start, Hexagon target, int N)
    {
        List<Vector3Int> result = new List<Vector3Int>();
        Vector3Int sC = CoordinateConversion.OffsetToCube(new Vector2Int(start.Column, start.Row));
        Vector3Int tC = CoordinateConversion.OffsetToCube(new Vector2Int(target.Column, target.Row));

        for (int i = 0; i <= N; i++)
        {
            result.Add(CubeRound(CubeLerp(sC, tC, 1.0f / N * i)));
        }
        return result;
    }

    private Vector3Int CubeRound(Vector3 frac)
    {
        int q = (int)Math.Round(frac.x);
        int r = (int)Math.Round(frac.y);
        int s = (int)Math.Round(frac.z);

        double q_diff = Math.Abs(q - frac.x);
        double r_diff = Math.Abs(r - frac.y);
        double s_diff = Math.Abs(s - frac.z);

        if (q_diff > r_diff && q_diff > s_diff)
        {
            q = -r - s;
        }
        else if (r_diff > s_diff)
        {
            r = -q - s;
        }
        else
        {
            s = -q - r;
        }

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
}
