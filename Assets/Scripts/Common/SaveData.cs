using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
            string spritePath = AssetDatabase.GetAssetPath(player.portrait.sprite);
            PlayerSaveData playerSaveData = new PlayerSaveData()
            {
                Name = player.Name,
                DirectControl = player.DirectControl,
                TargetGridCoord = player.TargetGridCoords,
                WorldCoord = player.transform.position,
                SpritePath = spritePath.Remove(spritePath.Length - 4, 4).Remove(0, 17)
            };
            PlayerListData.Add(playerSaveData);
        }
        WorldMapData = worldMap;
    }
}
