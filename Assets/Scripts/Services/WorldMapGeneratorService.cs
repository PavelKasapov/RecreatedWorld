using UnityEngine;

public class WorldMapGeneratorService
{
    private WorldMapMaganer _worldMapMaganer;

    public WorldMapGeneratorService(WorldMapMaganer worldMapMaganer)
    {
        _worldMapMaganer = worldMapMaganer;
    }
    public void GenerateMap(int mapHeight, int mapWidth)
    {
        _worldMapMaganer.WorldMap.Clear();
        for (int i = (-mapHeight / 2); i < (mapHeight / 2 + mapHeight % 2); i++)
        {
            for (int j = (-mapWidth / 2); j < (mapWidth / 2 + mapWidth % 2); j++)
            {
                WorldTile tile = new WorldTile()
                {
                    TerrainType = (TerrainType)Random.Range(0, 3),
                    Coords = new Vector3Int(i, j, 0)
                };
                _worldMapMaganer.WorldMap.Add(tile);
            }
        }
    }
}
