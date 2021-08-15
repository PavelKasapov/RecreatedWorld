using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldMapRenderService
{
    private readonly TileBase[] tileBases;

    private WorldMapMaganer _worldMapMaganer;
    private Tilemap _groundTilemap;
    private Dictionary<TerrainType, string> TilebaseNamePrefix = new Dictionary<TerrainType, string>
    {
        #region [TerrainType.CurrentType] = "name-prefix-"
        [TerrainType.Water] = "tilebase-water-",
        [TerrainType.Grassland] = "tilebase-grassland-",
        [TerrainType.Forest] = "tilebase-forest-",
        [TerrainType.Mountains] = "tilebase-mountains-",
        [TerrainType.Desert] = "tilebase-desert-",
        [TerrainType.Hills] = "tilebase-hills-",
        [TerrainType.Swamp] = "tilebase-swamp-",
        [TerrainType.Wastelands] = "tilebase-wastelands-",
        [TerrainType.Void] = "tilebase-void-",
        [TerrainType.Base] = "tilebase-base-"
        #endregion
    };

    public WorldMapRenderService(WorldMapMaganer worldMapMaganer, Tilemap tilemap)
    {
        _worldMapMaganer = worldMapMaganer;
        _groundTilemap = tilemap;
        tileBases = Resources.LoadAll("Tilebases", typeof(TileBase)).Cast<TileBase>().ToArray();
    }
    public void RenderMap()
    {
        _groundTilemap.ClearAllTiles();
        foreach (var tile in _worldMapMaganer.WorldMap)
        {
            string tilebaseName = TilebaseNamePrefix[tile.TerrainType] + Random.Range(1, 5);
            _groundTilemap.SetTile(tile.OffsetCoords, tileBases.FirstOrDefault(tilebase => tilebase.name == tilebaseName));
        }
    }
}
