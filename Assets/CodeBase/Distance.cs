using System;
using UnityEngine;

public static class Distance
{
    public static int GetOffsetDistance(Vector2Int selected, Vector2Int target)
    {
        Vector2Int selectedCoords = CoordinateConversion.OffsetToAxial(selected);
        Vector2Int targetCoords = CoordinateConversion.OffsetToAxial(target);
        return GetInlineAxialDistance(selected, target);
    }

    public static int GetAxialDistance(Vector2Int first, Vector2Int second)
    {
        Vector3Int fCubeCoord = CoordinateConversion.AxialToCube(first);
        Vector3Int sCubeCoord = CoordinateConversion.AxialToCube(second);
        return GetCubeDistance(fCubeCoord, sCubeCoord);
    }

    public static int GetInlineAxialDistance(Vector2Int first,Vector2Int second)
    {
        return (Math.Abs(first.x - second.x) + Math.Abs(first.x + first.y - second.x - second.y) + Math.Abs(first.y - second.y)) / 2;
    }

    public static int GetAnotherAxielDistance(Vector2Int first, Vector2Int second)
    {
        var vec = Subtract(first, second);
        return (Math.Abs(vec.x) + Math.Abs(vec.x + vec.y) + Math.Abs(vec.y)) / 2;
    }

    public static int GetCubeDistance(Vector3Int f, Vector3Int s)
    {
        Vector3Int vec = Subtract(f, s);
        return Mathf.Max(Mathf.Abs(vec.x), Mathf.Abs(vec.y), Mathf.Abs(vec.z));
    }

    public static Vector3Int Subtract(Vector3Int first, Vector3Int second)
    {
        return new Vector3Int(first.x - second.x, first.y - second.y, first.z - second.z);
    }

    public static Vector2Int Subtract(Vector2Int first, Vector2Int second)
    {
        return new Vector2Int(first.x - second.x, first.y - second.y);
    }
}