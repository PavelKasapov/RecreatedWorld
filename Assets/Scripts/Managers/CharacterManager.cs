using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;
    public GameObject characterPrefab;
    public ScriptableCharacter scriptablePlayerCharacter;
    public List<Player> PlayerList { get; set; }

    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CharacterManager>();
            }
            return _instance;
        }
    }
    void Awake()
    {
        PlayerList = new List<Player>();
    }
    public void SpawnPlayer(string name, bool directControl, string spritePath, Vector3Int targetGridCoord, Vector3 worldCoord) 
    {
        Player newPlayer = Instantiate(characterPrefab, worldCoord, Quaternion.identity).GetComponent<Player>();
        newPlayer.Name = name;
        newPlayer.DirectControl = directControl;
        newPlayer.portrait.sprite = Resources.Load<Sprite>(spritePath);
        PlayerList.Add(newPlayer);
        newPlayer.SetMovePoint(targetGridCoord);
        if (directControl)
        {
            GlobalStateManager.Instance.ControlledPlayer = newPlayer;
        }
    }

    private void SpawnCharacter(Player characterToSpawn)
    {
        Player newPlayer = Instantiate(characterPrefab, new Vector3(0, 0), Quaternion.identity).GetComponent<Player>();
        newPlayer.Name = name;
        newPlayer.portrait.sprite = scriptablePlayerCharacter.portraitSprite;
    }

}
