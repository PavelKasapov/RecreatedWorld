using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestExecutor : MonoBehaviour
{
    public TileSelector tileSelector;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Testing");

        WorldMapMaganer.Instance.GenerateMap(40, 40);
        CharacterManager.Instance.SpawnPlayer("Gector", true, "Sprites/PortraitSample", new Vector3Int(0, 0, 0), new Vector3(0, 0, 0));
        tileSelector.SelectTile(new Vector2(50, 100));
        GlobalStateManager.Instance.UnpauseGlobalMap();
        StartCoroutine(SaveAndLoadRoutine());

    }

    private IEnumerator SaveAndLoadRoutine()
    {
        yield return new WaitForSeconds(1);
        GlobalStateManager.Instance.SaveGame();
        Debug.Log("Save");
        yield return new WaitForSeconds(1);
        GlobalStateManager.Instance.LoadGame();
        Debug.Log("Load");
        GlobalStateManager.Instance.UnpauseGlobalMap();
        yield return new WaitForSeconds(2.5f);
        GlobalStateManager.Instance.LoadGame();
        Debug.Log("Load");
    }
}
