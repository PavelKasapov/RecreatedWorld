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
                var terrainType = (TerrainType)Random.Range(0, 3);
                WorldTile tile = new WorldTile(i, j, terrainType);
                _worldMapMaganer.WorldMap.Add(tile);
            }
        }
    }
}
