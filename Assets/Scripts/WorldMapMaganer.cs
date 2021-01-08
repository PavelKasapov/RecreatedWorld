﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldMapMaganer : MonoBehaviour
{
    private static WorldMapMaganer _instance;
    private List<WorldTile> _worldMap = new List<WorldTile>();

    public List<WorldTile> WorldMap
    {
        get 
        {
            return _worldMap;
        } 
        private set
        {
            _worldMap = value;
        }
    }

    public Tilemap groundLayout;
    public Tilemap locationsLayout;

    #region TileBases
    public TileBase water;
    public TileBase grassland;
    public TileBase forest;
    #endregion

    Dictionary<TerrainType, TileBase> TerrainTileBase;

    public static WorldMapMaganer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<WorldMapMaganer>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        TerrainTileBase = new Dictionary<TerrainType, TileBase>
        {
            #region [TerrainType.CurrentType] = machedTileBase
            [TerrainType.Water] = water,
            [TerrainType.Grassland] = grassland,
            [TerrainType.Forest] = forest,
            #endregion
        };
        DontDestroyOnLoad(gameObject);
    }

    public void GenerateMap(int mapHeight, int mapWidth)
    {
        for (int i = (- mapHeight / 2); i < (mapHeight / 2 + mapHeight % 2); i++)
        {
            for (int j = ( - mapWidth / 2); j < (mapWidth / 2 + mapWidth % 2); j++)
            {
                WorldTile tile = new WorldTile()
                {
                    TerrainType = (TerrainType)Random.Range(0, 3),
                    Coords = new Vector3Int(i, j, 0)
                };
                WorldMap.Add(tile);
            }
        }
        RenderMap();
    }

    public void RenderMap()
    {
        foreach (var tile in WorldMap)
        {
            groundLayout.SetTile(tile.Coords, TerrainTileBase[tile.TerrainType]);
        }

    }
}