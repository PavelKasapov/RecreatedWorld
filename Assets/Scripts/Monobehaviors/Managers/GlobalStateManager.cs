using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStateManager : MonoBehaviour
{
    private static GlobalStateManager _instance;
    public Player ControlledPlayer { get; set; }
    public bool IsMenuOpened { get; set; } = true;
    public bool IsGlobalMapPaused { get; set; } = true;

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
        IsGlobalMapPaused = true;
        SaveData data = SaveSystem.LoadGame();
        WorldMapMaganer.Instance.WorldMap = data.WorldMapData;
        foreach (Player player in CharacterManager.Instance.PlayerList)
        {
            Destroy(player.gameObject);
        }
        CharacterManager.Instance.PlayerList.Clear();
        foreach (PlayerSaveData player in data.PlayerListData) 
        {
            CharacterManager.Instance.SpawnPlayer(player.Name, player.DirectControl, player.SpritePath, player.WorldCoord, player.TargetGridCoord);
        }
        WorldMapMaganer.Instance.RenderMap();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
