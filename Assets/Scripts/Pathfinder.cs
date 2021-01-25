using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Pathfinder
{
    private static List<PathNode> _openList = new List<PathNode>();
    private static List<PathNode> _closedList = new List<PathNode>();
    private static Vector3Int _toCoords;
    private static PathNode _endPathNode = null;

    private static Dictionary<TerrainType, int> TerrainMoveCost = new Dictionary<TerrainType, int>
    {
        #region [TerrainType.CurrentType] = machedTileBase
        [TerrainType.Water] = 0,
        [TerrainType.Grassland] = 1,
        [TerrainType.Forest] = 2,
        #endregion
    };
    public static List<PathNode> FindPath(Vector3Int fromCoords, Vector3Int toCoords)
    {
        List<PathNode> path = new List<PathNode>();
        _toCoords = toCoords; 
        int newHCost = CalculateHCost(fromCoords, _toCoords);
        int newSelfCost = TerrainMoveCost[WorldMapMaganer.Instance.WorldMap.Find(tile => tile.Coords == fromCoords).TerrainType];
        PathNode startNode = new PathNode()
        {
            Coords = fromCoords,
            GCost = 0,
            prevNode = null,
            FCost = newHCost + newSelfCost,
            SelfCost = newSelfCost
        };
        _openList.Add(startNode);
        while ((_openList.Count > 0) && (_endPathNode == null))
        {
            PathNode lowestFCostNode = _openList.OrderBy(node => node.FCost).FirstOrDefault();
            CheckNeibhourTiles(lowestFCostNode);
            _openList.Remove(lowestFCostNode);
            _closedList.Add(lowestFCostNode);
        }
        if (_endPathNode != null)
        {
            path.Add(_endPathNode);
            PathNode currentNode = _endPathNode.prevNode;
            while (currentNode != null)
            {
                path.Add(currentNode);
                currentNode = currentNode.prevNode;
            }
        }
        _openList.Clear();
        _closedList.Clear();
        _endPathNode = null;
        path.Reverse();
        path.Remove(path[0]);
        return path;
    }

    private static void CheckNeibhourTiles(PathNode tile)
    {
        List<Vector3Int> neighbourCoords = new List<Vector3Int>();
        if (!_closedList.Exists(closedTile => closedTile.Coords == tile.Coords))
        {
            neighbourCoords.Add(new Vector3Int(tile.Coords.x + 1, tile.Coords.y, 0));
            neighbourCoords.Add(new Vector3Int(tile.Coords.x - 1, tile.Coords.y, 0));
            neighbourCoords.Add(new Vector3Int(tile.Coords.x, tile.Coords.y + 1, 0));
            neighbourCoords.Add(new Vector3Int(tile.Coords.x, tile.Coords.y - 1, 0));

            if (tile.Coords.y % 2 == 0)
            {
                neighbourCoords.Add(new Vector3Int(tile.Coords.x - 1, tile.Coords.y + 1, 0));
                neighbourCoords.Add(new Vector3Int(tile.Coords.x - 1, tile.Coords.y - 1, 0));
            }
            else
            {
                neighbourCoords.Add(new Vector3Int(tile.Coords.x + 1, tile.Coords.y + 1, 0));
                neighbourCoords.Add(new Vector3Int(tile.Coords.x + 1, tile.Coords.y - 1, 0));
            }
        }
        foreach (Vector3Int coords in neighbourCoords)
        {
            if (!_closedList.Exists(closedTile => closedTile.Coords == coords))
            {
                int newHCost = CalculateHCost(coords, _toCoords);
                int newSelfCost = TerrainMoveCost[WorldMapMaganer.Instance.WorldMap.Find(t => t.Coords == coords).TerrainType];
                
                int newGCost = tile.GCost + newSelfCost;
                PathNode newNode = new PathNode()
                {
                    Coords = coords,
                    GCost = newGCost,
                    prevNode = tile,
                    FCost = newHCost + newGCost,
                    SelfCost = newSelfCost
                };
                if (coords == _toCoords)
                {
                    _endPathNode = newNode;
                }
                if (newSelfCost != 0)
                {
                    _openList.Add(newNode);
                }
                else
                {
                    _closedList.Add(newNode);
                }
                
            }
        }
    }

    private static int CalculateHCost(Vector3Int fromCoord, Vector3Int toCoord)
    {
        int HCost = 0;
        int distance = GetTileDistance(fromCoord, toCoord);
        for (int i = 1; i <= distance; i++)
        {
            Vector3 newWorldCoord = Vector3.Lerp(WorldMapMaganer.Instance.grid.GetCellCenterWorld(fromCoord), WorldMapMaganer.Instance.grid.GetCellCenterWorld(toCoord), 1.0f * i / distance);
            Vector3Int newCellCoord = WorldMapMaganer.Instance.grid.WorldToCell(newWorldCoord);
            HCost += TerrainMoveCost[WorldMapMaganer.Instance.WorldMap.Find(tile => tile.Coords == newCellCoord).TerrainType];
        }
        return HCost;
    }

    private static int GetTileDistance(Vector3Int fromCoord, Vector3Int toCoord)
    {
        int dx = toCoord.x - fromCoord.x;     // signed deltas
        int dy = toCoord.y - fromCoord.y;
        int x = Mathf.Abs(dx);  // absolute deltas
        int y = Mathf.Abs(dy);
        // special case if we start on an odd row or if we move into negative x direction
        if ((dx < 0) ^ ((fromCoord.y & 1) == 1))
            x = Mathf.Max(0, x - (y + 1) / 2);
        else
            x = Mathf.Max(0, x - (y) / 2);
        return x + y;
    }
}
