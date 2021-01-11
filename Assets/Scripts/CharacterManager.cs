using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;
    public GameObject characterPrefab;
    public Character player;
    public List<Player> PlayerList;

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
        PlayerList.Clear();
        DontDestroyOnLoad(gameObject);
    }
    public void CreatePlayer(string name) 
    {
        Player newPlayer = Instantiate(characterPrefab, new Vector3(0, 0), Quaternion.identity).GetComponent<Player>() as Player;
        newPlayer.Name = name;
        newPlayer.portrait.sprite = player.portraitSprite;
        Debug.Log(PlayerList);
        PlayerList.Add(newPlayer);
    }
}
