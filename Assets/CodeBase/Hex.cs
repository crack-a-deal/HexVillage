#pragma warning disable CS0659 // Тип переопределяет Object.Equals(object o), но не переопределяет Object.GetHashCode()
using System.Collections.Generic;
using UnityEngine;

public struct Hex
#pragma warning restore CS0659 // Тип переопределяет Object.Equals(object o), но не переопределяет Object.GetHashCode()
{
    public int Q { get; }
    public int R { get; }
    public int S { get; }

    // Cube storage
    public Hex(int q, int r, int s)
    {
        Q = q;
        R = r;
        S = s;
    }

    Hex Add(Hex firstHex, Hex secondHex)
    {
        return new Hex(firstHex.Q + secondHex.Q, firstHex.R + secondHex.R, firstHex.S + secondHex.S);
    }

    Hex Subtract(Hex firstHex, Hex secondHex)
    {
        return new Hex(firstHex.Q - secondHex.Q, firstHex.R - secondHex.R, firstHex.S - secondHex.S);
    }

    int Length(Hex hex)
    {
        return ((Mathf.Abs(hex.Q) + Mathf.Abs(hex.R) + Mathf.Abs(hex.S)) / 2);
    }

    //TODO: pick one method for calculate distance between two hexes

    //int Distance(Hex a, Hex b)
    //{
    //    Hex vec = Subtract(a, b);
    //    return Mathf.Max(Mathf.Abs(vec.Q), Mathf.Abs(vec.R), Mathf.Abs(vec.S));
    //}

    int Distance(Hex firstHex, Hex secondHex)
    {
        return Length(Subtract(firstHex, secondHex));
    }

    Hex HexDirection(int direction)
    {
        if (direction < 0 || direction > 5)
            throw new System.Exception("Direction index out of array");

        Dictionary<int, Hex> keyValuePairs = new Dictionary<int, Hex>
        {
            { 0, new Hex(1, 0, -1) },
            { 1, new Hex(1, -1, 0) },
            { 2, new Hex(0, -1, 1) },
            { 3, new Hex(-1, 0, 1) },
            { 4, new Hex(-1, 1, 0) },
            { 5, new Hex(0, 1, -1) },
        };

        return keyValuePairs[direction];
    }

    Hex Neighbor(Hex hex, int direction)
    {
        return hex.Add(hex, HexDirection(direction));
    }

    public override bool Equals(object obj)
    {
        return obj is Hex other && Equals(other);
    }

    public bool Equals(Hex hex) => Q == hex.Q && R == hex.R && S == hex.S;

    public override string ToString()
    {
        return $"Hex ({Q}, {R}, {S})";
    }
}