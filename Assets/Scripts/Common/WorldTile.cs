[System.Serializable]
public class WorldTile
{
    public WorldTile(int xAxisCoord, int yAxisCoord, TerrainType terrainType)
    {
        OffsetCoords = new SerializableVector3Int(xAxisCoord, yAxisCoord, 0);
        CubicCoords = CalculateCubicCoords(xAxisCoord, yAxisCoord);
        TerrainType = terrainType;
    }

    public SerializableVector3Int OffsetCoords { get; }
    public SerializableVector3Int CubicCoords { get; }
    public TerrainType TerrainType { get;  }

    private SerializableVector3Int CalculateCubicCoords(int xAxisCoord, int yAxisCoord)
    {
        /* Conversion from: https://www.redblobgames.com/grids/hexagons/#conversions */
        int x = xAxisCoord - (yAxisCoord + (yAxisCoord & 1)) / 2;
        int z = yAxisCoord;
        int y = -(x + z);

        return new SerializableVector3Int(x, y, z);
    }
}
