using UnityEngine;

[System.Serializable]
public class WorldTile
{
    public TerrainType TerrainType { get; set; }
    public SerializableVector3Int Coords { get; set; }
}
