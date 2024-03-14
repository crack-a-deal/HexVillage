using UnityEngine;

public static class Distance
{
    public static int GetOffsetDistance(Vector2Int selected, Vector2Int target)
    {
        Vector2Int selectedCoords = CoordinateConversion.OffsetToAxial(selected);
        Vector2Int targetCoords = CoordinateConversion.OffsetToAxial(target);
        return GetAxialDistance(selected, target);
    }

    public static int GetAxialDistance(Vector2Int first, Vector2Int second)
    {
        Vector3Int fCubeCoord = CoordinateConversion.AxialToCube(first);
        Vector3Int sCubeCoord = CoordinateConversion.AxialToCube(second);
        return GetCubeDistance(fCubeCoord, sCubeCoord);
    }

    public static int GetCubeDistance(Vector3Int f, Vector3Int s)
    {
        Vector3Int vec = CubeSubtract(f, s);
        return Mathf.Max(Mathf.Abs(vec.x), Mathf.Abs(vec.y), Mathf.Abs(vec.z));
    }

    public static Vector3Int CubeSubtract(Vector3Int first, Vector3Int second)
    {
        return new Vector3Int(first.x - second.x, first.y - second.y, first.z - second.z);
    }
}