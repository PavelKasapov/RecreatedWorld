using System.IO;
using UnityEditor;

[System.Serializable]
public class PlayerSaveData
{
    public string Name { get; set; }
    public bool HasControl { get; set; }
    public string SpritePath { get; set; }
    public SerializableVector3Int TargetGridCoord { get; set; }
    public SerializableVector3 WorldCoord { get; set; }
    //public SerializableVector3Int GridCoord { get; set; }
    public PlayerSaveData(Player player)
    {
        Name = player.Name;
        HasControl = player.HasControl;
        TargetGridCoord = player.TargetGridCoords;
        WorldCoord = player.transform.position;
        string spritePath = AssetDatabase.GetAssetPath(player.portrait.sprite);
        SpritePath = Path.GetFileNameWithoutExtension(spritePath);
    }
}
