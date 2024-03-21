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

    //TODO: use + operator
    Hex Add(Hex firstHex, Hex secondHex)
    {
        return new Hex(firstHex.Q + secondHex.Q, firstHex.R + secondHex.R, firstHex.S + secondHex.S);
    }

    //TODO: use - operator
    Hex Subtract(Hex firstHex, Hex secondHex)
    {
        return new Hex(firstHex.Q - secondHex.Q, firstHex.R - secondHex.R, firstHex.S - secondHex.S);
    }

    int Length(Hex hex)
    {
        return ((Mathf.Abs(hex.Q) + Mathf.Abs(hex.R) + Mathf.Abs(hex.S)) / 2);
    }

    int Distance(Hex firstHex, Hex secondHex)
    {
        return Length(Subtract(firstHex, secondHex));
    }

    public static Hex HexDirection(int direction)
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

    public static Hex Neighbor(Hex hex, int direction)
    {
        return hex.Add(hex, HexDirection(direction));
    }

    public static Hex[] Neighbors(Hex hex)
    {
        Hex[] result = new Hex[6];
        for (int i = 0; i < 6; i++)
        {
            result[i]=hex.Add(hex,HexDirection(i));
        }
        return result;
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