using System;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public List<Vector2Int> GetLinePath(Hexagon startHexagon, Hexagon targetHexagon)
    {
        int distanceBetweenHexes = Distance.GetOffsetDistance(startHexagon.Coordinate, targetHexagon.Coordinate);
        List<Vector3Int> hexesPositions = CalculateHexPath(startHexagon, targetHexagon, distanceBetweenHexes);
        List<Vector2Int> result = new List<Vector2Int>(distanceBetweenHexes);
        foreach (var position in hexesPositions)
        {
            Vector2Int offsetCoordinates = CoordinateConversion.CubeToOffset(position);
            result.Add(offsetCoordinates);
        }
        return result;
    }

    private List<Vector3Int> CalculateHexPath(Hexagon startHexagon, Hexagon targetHexagon, int distanceBetweenHexes)
    {
        List<Vector3Int> positions = new List<Vector3Int>(distanceBetweenHexes + 1);
        Vector3Int startCoordinates = CoordinateConversion.OffsetToCube(new Vector2Int(startHexagon.Coordinate.x, startHexagon.Coordinate.y));
        Vector3Int targetCoordinates = CoordinateConversion.OffsetToCube(new Vector2Int(targetHexagon.Coordinate.x, targetHexagon.Coordinate.y));

        for (int i = 0; i <= distanceBetweenHexes; i++)
        {
            positions.Add(RoundCubeCoordinate(LerpCubeCoordinate(startCoordinates, targetCoordinates, 1.0f / distanceBetweenHexes * i)));
        }

        return positions;
    }

    private Vector3Int RoundCubeCoordinate(Vector3 floatingPoint)
    {
        int q = (int)Math.Round(floatingPoint.x);
        int r = (int)Math.Round(floatingPoint.y);
        int s = (int)Math.Round(floatingPoint.z);

        double q_diff = Math.Abs(q - floatingPoint.x);
        double r_diff = Math.Abs(r - floatingPoint.y);
        double s_diff = Math.Abs(s - floatingPoint.z);

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

    private Vector3 LerpCubeCoordinate(Vector3Int firstPoint, Vector3Int secondPoint, float t)
    {
        Vector3 result = new Vector3(
            Mathf.Lerp(firstPoint.x, secondPoint.x, t),
            Mathf.Lerp(firstPoint.y, secondPoint.y, t),
            Mathf.Lerp(firstPoint.z, secondPoint.z, t)
            );
        return result;
    }
}
