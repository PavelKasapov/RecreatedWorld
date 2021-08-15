using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PathfinderService
{
    private List<PathNode> _openList = new List<PathNode>();
    private List<PathNode> _closedList = new List<PathNode>();
    private Vector3Int _toCoords;
    private PathNode _endPathNode = null;

    private static Dictionary<TerrainType, int> TerrainMoveCost = new Dictionary<TerrainType, int>
    {
        #region [TerrainType.CurrentType] = machedTileBase
        [TerrainType.Water] = 0,
        [TerrainType.Grassland] = 1,
        [TerrainType.Forest] = 2,
        #endregion
    };

    private WorldMapMaganer _worldMapMaganer;
    private Grid _worldGrid;

    public PathfinderService(WorldMapMaganer worldMapMaganer, Grid worldGrid)
    {
        _worldMapMaganer = worldMapMaganer;
        _worldGrid = worldGrid;
    }

    public List<PathNode> FindPath(Vector3Int fromOffsetCoords, Vector3Int toOffsetCoords)
    {
        var path = new List<PathNode>();
        var fromTile = _worldMapMaganer.WorldMap.FirstOrDefault(tile => tile.OffsetCoords == fromOffsetCoords);
        var toTile = _worldMapMaganer.WorldMap.Find(tile => tile.OffsetCoords == toOffsetCoords);
        _toCoords = toOffsetCoords;
        int newHCost = CalculateHCost(fromOffsetCoords, _toCoords);
        int newSelfCost = TerrainMoveCost[_worldMapMaganer.WorldMap.Find(tile => tile.OffsetCoords == fromOffsetCoords).TerrainType];
        PathNode startNode = new PathNode(fromOffsetCoords, 0, newHCost, newSelfCost, null);
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
            PathNode currentNode = _endPathNode.PrevNode;
            while (currentNode != null)
            {
                path.Add(currentNode);
                currentNode = currentNode.PrevNode;
            }
        }
        _openList.Clear();
        _closedList.Clear();
        _endPathNode = null;
        path.Reverse();
        path.Remove(path[0]);
        return path;
    }

    private void CheckNeibhourTiles(PathNode prevNode)
    {
        List<Vector3Int> neighbourCoords = new List<Vector3Int>();
        if (!_closedList.Exists(closedTile => closedTile.Coords == prevNode.Coords))
        {
            neighbourCoords.Add(new Vector3Int(prevNode.Coords.x + 1, prevNode.Coords.y, 0));
            neighbourCoords.Add(new Vector3Int(prevNode.Coords.x - 1, prevNode.Coords.y, 0));
            neighbourCoords.Add(new Vector3Int(prevNode.Coords.x, prevNode.Coords.y + 1, 0));
            neighbourCoords.Add(new Vector3Int(prevNode.Coords.x, prevNode.Coords.y - 1, 0));

            if (prevNode.Coords.y % 2 == 0)
            {
                neighbourCoords.Add(new Vector3Int(prevNode.Coords.x - 1, prevNode.Coords.y + 1, 0));
                neighbourCoords.Add(new Vector3Int(prevNode.Coords.x - 1, prevNode.Coords.y - 1, 0));
            }
            else
            {
                neighbourCoords.Add(new Vector3Int(prevNode.Coords.x + 1, prevNode.Coords.y + 1, 0));
                neighbourCoords.Add(new Vector3Int(prevNode.Coords.x + 1, prevNode.Coords.y - 1, 0));
            }
        }
        foreach (Vector3Int coords in neighbourCoords)
        {
            if (!_closedList.Exists(closedTile => closedTile.Coords == coords))
            {
                int newHCost = CalculateHCost(coords, _toCoords);
                int newSelfCost = TerrainMoveCost[_worldMapMaganer.WorldMap.Find(t => t.OffsetCoords == coords).TerrainType];
                
                int newGCost = prevNode.GCost + newSelfCost;
                PathNode newNode = new PathNode(coords, newGCost, newHCost, newSelfCost, prevNode);
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

    private int CalculateHCost(Vector3Int fromCoord, Vector3Int toCoord)
    {
        int HCost = 0;
        int distance = GetTileDistance(fromCoord, toCoord);
        for (int i = 1; i <= distance; i++)
        {
            Vector3 newWorldCoord = Vector3.Lerp(_worldGrid.GetCellCenterWorld(fromCoord), _worldGrid.GetCellCenterWorld(toCoord), 1.0f * i / distance);
            Vector3Int newCellCoord = _worldGrid.WorldToCell(newWorldCoord);
            HCost += TerrainMoveCost[_worldMapMaganer.WorldMap.Find(tile => tile.OffsetCoords == newCellCoord).TerrainType];
        }
        return HCost;
    }

    //private int CalculateHCost(Vector3Int fromCoord, Vector3Int toCoord)
    //{
    //    int HCost = 0;
    //    int distance = GetTileDistance(fromCoord, toCoord);
    //    for (int i = 1; i <= distance; i++)
    //    {
    //        Vector3 newWorldCoord = Vector3.Lerp(_worldGrid.GetCellCenterWorld(fromCoord), _worldGrid.GetCellCenterWorld(toCoord), 1.0f * i / distance);
    //        Vector3Int newCellCoord = _worldGrid.WorldToCell(newWorldCoord);
    //        HCost += TerrainMoveCost[_worldMapMaganer.WorldMap.Find(tile => tile.OffsetCoords == newCellCoord).TerrainType];
    //    }
    //    return HCost;
    //}

    private int GetTileDistance(Vector3Int fromCoord, Vector3Int toCoord)
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
