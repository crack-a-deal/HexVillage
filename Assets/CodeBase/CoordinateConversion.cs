using UnityEngine;

public static class CoordinateConversion
{
    // Cube coordinates
    public static Vector3Int AxialToCube(Vector2Int hexagon)
    {
        var q = hexagon.x;
        var r = hexagon.y;
        var s = -q - r;
        return new Vector3Int(q, r, s);
    }

    public static Vector3Int OffsetToCube(Vector2Int hexagon)
    {
        var q = hexagon.x;
        var r = hexagon.y - (hexagon.x - (hexagon.x & 1)) / 2;
        return new Vector3Int(q, r, -q - r);
    }

    // Axial coordinates
    public static Vector2Int CubeToAxiel(Vector3Int hexagon)
    {
        var q = hexagon.x;
        var r = hexagon.y;
        return new Vector2Int(q, r);
    }

    public static Vector2Int OffsetToAxial(Vector2Int hexagon)
    {
        var q = hexagon.x;
        var r = hexagon.y - (hexagon.x - (hexagon.x & 1)) / 2;
        return new Vector2Int(q, r);
    }

    // Offset coordinates
    public static Vector2Int CubeToOffset(Vector3Int hexagon)
    {
        var q = hexagon.x;
        var r = Mathf.Max(0, hexagon.y + (hexagon.x - (hexagon.x & 1)) / 2);
        return new Vector2Int(q, r);
    }


    public static Vector2Int AxielToOffset(Vector2Int hexagon)
    {
        var q = hexagon.x;
        var r = hexagon.y + (hexagon.x - (hexagon.x & 1)) / 2;
        return new Vector2Int(q, r);
    }
}
