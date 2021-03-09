using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Zenject;

public class SaveService
{
    private GlobalStateManager _globalStateManager;
    private WorldMapMaganer _worldMapManager;
    private CharacterManager _characterManager;
    private PlayerFactory _playerFactory;
    private WorldMapRenderService _renderService;

    [Inject]
    public void Construct(GlobalStateManager globalStateManager, WorldMapMaganer worldMapMaganer, CharacterManager characterManager,
        PlayerFactory playerFactory, WorldMapRenderService renderService)
    {
        _globalStateManager = globalStateManager;
        _worldMapManager = worldMapMaganer;
        _characterManager = characterManager;
        _playerFactory = playerFactory;
        _renderService = renderService;
    }

    public void SaveGame ()
    {
        SerializeData(_characterManager.PlayerList, _worldMapManager.WorldMap);
    }

    public void LoadGame()
    {
        _globalStateManager.IsGlobalMapPaused = true;
        SaveData data = DeserializeData();

        _worldMapManager.WorldMap = data.WorldMapData;
        _renderService.RenderMap();

        foreach (Player player in _characterManager.PlayerList)
        {
            GameObject.Destroy(player.gameObject);
        }
        _characterManager.PlayerList.Clear();
        foreach (PlayerSaveData player in data.PlayerListData)
        {
            Player newPlayer = _playerFactory.Create(player);
            _characterManager.PlayerList.Add(newPlayer);
        }
    }

    private void SerializeData(List<Player> playerList, List<WorldTile> worldMap)
    { 
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saves/testsave.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(playerList, worldMap);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    private SaveData DeserializeData()
    {
        string path = Application.persistentDataPath + "/saves/testsave.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
