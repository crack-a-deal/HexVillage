using System.Collections.Generic;
using UnityEngine;

namespace HexagonGrid
{
    public class HexGridLayout
    {
        public int Columns { get; private set; }
        public int Rows { get; private set; }
        public Hex[,] Grid { get; private set; }

        public void CreateLayoutGrid(int columnsCount, int rowsCount)
        {
            Columns = columnsCount;
            Rows = rowsCount;
            Grid = new Hex[Columns, Rows];

            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    Vector3Int cubeCoordinates = CoordinateConversion.OffsetToCube(new Vector2Int(column, row));
                    Hex hexq = new Hex(cubeCoordinates.x, cubeCoordinates.y, cubeCoordinates.z);
                    Grid[column, row] = hexq;
                }
            }
        }

        public List<Hex> GetNeighborsList(Hex hex)
        {
            List<Hex> result = new List<Hex>(6);

            if (hex.OffsetCoordinates.x % 2 == 0)
            {
                result = GetEvenNeigbors(hex.OffsetCoordinates);
            }
            else
            {
                result = GetOddNeigbors(hex.OffsetCoordinates);
            }

            result.RemoveAll(item => item.Q == -1 && item.R == -1 && item.S == -1);
            return result;
        }

        private List<Hex> GetEvenNeigbors(Vector2Int hexagonCoordinates)
        {
            List<Hex> result = new List<Hex>(6)
            {
                GetNeighbor(new Vector2Int(hexagonCoordinates.x + 1, hexagonCoordinates.y)),
                GetNeighbor(new Vector2Int(hexagonCoordinates.x + 1, hexagonCoordinates.y - 1)),
                GetNeighbor(new Vector2Int(hexagonCoordinates.x, hexagonCoordinates.y - 1)),
                GetNeighbor(new Vector2Int(hexagonCoordinates.x - 1, hexagonCoordinates.y - 1)),
                GetNeighbor(new Vector2Int(hexagonCoordinates.x - 1, hexagonCoordinates.y)),
                GetNeighbor(new Vector2Int(hexagonCoordinates.x, hexagonCoordinates.y + 1))
            };

            return result;
        }

        private List<Hex> GetOddNeigbors(Vector2Int hexagonCoordinates)
        {
            List<Hex> result = new List<Hex>(6)
            {
                GetNeighbor(new Vector2Int(hexagonCoordinates.x + 1, hexagonCoordinates.y + 1)),
                GetNeighbor(new Vector2Int(hexagonCoordinates.x + 1, hexagonCoordinates.y)),
                GetNeighbor(new Vector2Int(hexagonCoordinates.x, hexagonCoordinates.y - 1)),
                GetNeighbor(new Vector2Int(hexagonCoordinates.x - 1, hexagonCoordinates.y)),
                GetNeighbor(new Vector2Int(hexagonCoordinates.x - 1, hexagonCoordinates.y + 1)),
                GetNeighbor(new Vector2Int(hexagonCoordinates.x, hexagonCoordinates.y + 1))
            };

            return result;
        }

        private Hex GetNeighbor(Vector2Int hexagonCoord)
        {
            if (hexagonCoord.x < 0 || hexagonCoord.x >= Columns)
            {
                return new Hex(-1, -1, -1);
            }
            if (hexagonCoord.y < 0 || hexagonCoord.y >= Rows)
            {
                return new Hex(-1, -1, -1);
            }

            //TODO: move this
            //if (!_hexagonsGrid[hexagonCoord.x, hexagonCoord.y].IsWalkable)
            //{
            //    return null;
            //}

            return Grid[hexagonCoord.x, hexagonCoord.y];
        }
    }
}