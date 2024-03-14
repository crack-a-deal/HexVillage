using UnityEngine;

public static class CoordinateConversion
{
    public static Vector2Int OffsetToAxial(Vector2Int hexagon)
    {
        var q = hexagon.x;
        var r = hexagon.y - (hexagon.x - (hexagon.y & 1)) / 2;
        return new Vector2Int(q, r);
    }

    public static Vector3Int AxialToCube(Vector2Int hexagon)
    {
        var q = hexagon.x;
        var r = hexagon.y;
        var s = -q - r;
        return new Vector3Int(q, r, s);
    }

    public static Vector2Int CubeToOffset(Vector3Int hexagon)
    {
        var q = hexagon.x;
        //TODO: fix row conversion
        //r !< 0 || r!>rowcount
        var r = hexagon.y + (hexagon.x - (hexagon.x & 1)) / 2;
        if(r<0) r = 0;
        if(r>10) r = 10;
        return new Vector2Int(q, r);
    }

    public static Vector3Int OffsetToCube(Vector2Int hexagon)
    {
        var q = hexagon.x;
        var r = hexagon.y - (hexagon.x - (hexagon.x & 1)) / 2;
        return new Vector3Int(q, r, -q - r);
    }
}
