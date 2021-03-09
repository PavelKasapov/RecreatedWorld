using UnityEngine;
using Zenject;

public class PlayerFactory
{
    private const string PlayerPrefabPath = "Prefabs/PlayerPrefab";
    private readonly DiContainer _diContainer;

    private GameObject _playerPrefab;

    public PlayerFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
        _playerPrefab = Resources.Load(PlayerPrefabPath) as GameObject;
    }
    public Player Create(string name, bool hasControl, string spritePath, Vector3Int targetGridCoord, Vector3 worldCoord)
    {
        Player newPlayer = _diContainer.InstantiatePrefabForComponent<Player>(_playerPrefab, worldCoord, Quaternion.identity, null);
        newPlayer.Name = name;
        newPlayer.HasControl = hasControl;
        newPlayer.portrait.sprite = Resources.Load<Sprite>(spritePath);
        newPlayer.MovementController.SetMovePoint(targetGridCoord);
        return newPlayer;
    }

    public Player Create(PlayerSaveData saveData)
    {
        Player newPlayer = _diContainer.InstantiatePrefabForComponent<Player>(_playerPrefab, saveData.WorldCoord, Quaternion.identity, null);
        newPlayer.Name = saveData.Name;
        newPlayer.HasControl = saveData.HasControl;
        newPlayer.portrait.sprite = Resources.Load<Sprite>(saveData.SpritePath);
        newPlayer.MovementController.SetMovePoint(saveData.TargetGridCoord);
        return newPlayer;
    }

}
