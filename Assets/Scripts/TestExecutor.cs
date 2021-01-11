using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestExecutor : MonoBehaviour
{
    public TileSelector tileSelector;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Testing");

        Debug.Log("Test Map Generate Start");
        WorldMapMaganer.Instance.GenerateMap(40, 40);
        Debug.Log("Test Map Generate Complete");

        Vector3 testClick = new Vector2(150, 300);
        Debug.Log("Test trying to click on (" + testClick.x + ", " + testClick.y + ") mouse coords");
        tileSelector.SelectTile(testClick);
        Debug.Log("Success click");

        CharacterManager.Instance.CreatePlayer("Gector");
        CharacterManager.Instance.PlayerList.Find(x => x.Name == "Gector").Move();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
