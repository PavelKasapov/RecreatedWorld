using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStateManager : MonoBehaviour
{
    private static GlobalStateManager _instance;
    public float GlobalMapTimeScale { get; set; }
    public Player ControlledPlayer { get; set; }
    
    public static GlobalStateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GlobalStateManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(CharacterManager.Instance.PlayerList, WorldMapMaganer.Instance.WorldMap);
    }

    public void LoadGame()
    {
        this.PauseGlobalMap();
        SaveData data = SaveSystem.LoadGame();
        WorldMapMaganer.Instance.WorldMap = data.WorldMapData;
        foreach (Player player in CharacterManager.Instance.PlayerList)
        {
            Destroy(player.gameObject);
        }
        CharacterManager.Instance.PlayerList.Clear();
        foreach (PlayerSaveData player in data.PlayerListData) 
        {
            CharacterManager.Instance.SpawnPlayer(player.Name, player.DirectControl, player.SpritePath, player.TargetGridCoord, player.WorldCoord);
        }
        WorldMapMaganer.Instance.RenderMap();
    }

    public void PauseGlobalMap()
    {
        GlobalMapTimeScale = 0f;
    }

    public void UnpauseGlobalMap()
    {
        GlobalMapTimeScale = 1f;
    }
}
