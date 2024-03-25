using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct Hex
{
    private static readonly Dictionary<int, Hex> Directions = new Dictionary<int, Hex>
    {
        { 0, new Hex(1, 0, -1) },
        { 1, new Hex(1, -1, 0) },
        { 2, new Hex(0, -1, 1) },
        { 3, new Hex(-1, 0, 1) },
        { 4, new Hex(-1, 1, 0) },
        { 5, new Hex(0, 1, -1) },
    };

    public int Q { get; }
    public int R { get; }
    public int S { get; }

    public Vector2Int OffsetCoordinates => new Vector2Int(Q, Mathf.Max(0, R + (Q - (Q & 1)) / 2));

    #region Cube storage
    public Hex(int q, int r, int s)
    {
        Q = q;
        R = r;
        S = s;
    }
    #endregion

    #region Offset storage
    public Hex(int column, int row)
    {
        Vector3Int coordinates = CoordinateConversion.OffsetToCube(new Vector2Int(column, row));
        Q = coordinates.x;
        R = coordinates.y;
        S = coordinates.z;
    }

    public Hex(Vector2Int coord)
    {
        Vector3Int coordinates = CoordinateConversion.OffsetToCube(coord);
        Q = coordinates.x;
        R = coordinates.y;
        S = coordinates.z;
    }
    #endregion

    #region Operators
    public static Hex operator +(Hex firstHex, Hex secondHex)
        => new Hex(firstHex.Q + secondHex.Q, firstHex.R + secondHex.R, firstHex.S + secondHex.S);

    public static Hex operator -(Hex firstHex, Hex secondHex)
        => new Hex(firstHex.Q - secondHex.Q, firstHex.R - secondHex.R, firstHex.S - secondHex.S);

    public static bool operator ==(Hex firstHex, Hex secondHex)
        => firstHex.Equals(secondHex);

    public static bool operator !=(Hex firstHex, Hex secondHex)
        => !firstHex.Equals(secondHex);
    #endregion

    public static int Length(Hex hex)
    {
        return ((Mathf.Abs(hex.Q) + Mathf.Abs(hex.R) + Mathf.Abs(hex.S)) / 2);
    }

    public static int Distance(Hex firstHex, Hex secondHex)
    {
        return Length(firstHex - secondHex);
    }

    public static Hex HexDirection(int direction)
    {
        if (!Directions.ContainsKey(direction))
            throw new System.Exception("Direction index out of array");

        return Directions[direction];
    }

    public static Hex GetNeighborByDirection(Hex hex, int direction)
    {
        return hex + HexDirection(direction);
    }

    public static Hex[] GetNeighbors(Hex hex)
    {
        return Directions.Values.Select(dir => hex + dir).ToArray();
    }

    public override bool Equals(object obj)
    {
        return obj is Hex other && Equals(other);
    }

    public bool Equals(Hex hex)
        => Q == hex.Q && R == hex.R && S == hex.S;

    public override string ToString()
        => $"Hex ({Q}, {R}, {S})";

    public override int GetHashCode()
        => (Q, R, S).GetHashCode();
}
