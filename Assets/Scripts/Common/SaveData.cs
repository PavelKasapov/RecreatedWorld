using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public List<PlayerSaveData> PlayerListData { get; set; }
    public List<WorldTile> WorldMapData { get; set; }

    public SaveData(List<Player> playerList, List<WorldTile> worldMap)
    {
        PlayerListData = new List<PlayerSaveData>();
        foreach (Player player in playerList)
        {
            PlayerSaveData playerSaveData = new PlayerSaveData(player);
            PlayerListData.Add(playerSaveData);
        }
        WorldMapData = worldMap;
    }
}
