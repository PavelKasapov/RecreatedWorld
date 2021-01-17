using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestExecutor : MonoBehaviour
{
    public TileSelector tileSelector;
    public Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Testing");

        WorldMapMaganer.Instance.GenerateMap(40, 40);
        CharacterManager.Instance.SpawnPlayer("Gector", true, "Sprites/PortraitSample", new Vector3Int(5, 5, 0), new Vector3(0, 0, 0));
        GlobalStateManager.Instance.UnpauseGlobalMap();
        //StartCoroutine(TestSaveAndLoadRoutine());
        StartCoroutine(TestGoAroundRoutine());
        //List<PathNode> path = Pathfinder.FindPath(new Vector3Int(0, 0, 0), new Vector3Int(5, 5, 0));
        //GlobalStateManager.Instance.ControlledPlayer.SetMovePoint(new Vector3Int(2, 2, 0));

    }

    private IEnumerator TestSaveAndLoadRoutine()
    {
        yield return new WaitForSeconds(1);
        GlobalStateManager.Instance.SaveGame();
        Debug.Log("Save");
        yield return new WaitForSeconds(1);
        GlobalStateManager.Instance.LoadGame();
        Debug.Log("Load");
        GlobalStateManager.Instance.UnpauseGlobalMap();
        Debug.Log("Unpause");
        yield return new WaitForSeconds(2.5f);
        GlobalStateManager.Instance.LoadGame();
        Debug.Log("Load");
    }

    private IEnumerator TestGoAroundRoutine()
    {
        yield return new WaitForSeconds(2);
        GlobalStateManager.Instance.ControlledPlayer.SetMovePoint(new Vector3Int(2, 2, 0));
        yield return new WaitForSeconds(2);
        GlobalStateManager.Instance.ControlledPlayer.SetMovePoint(new Vector3Int(-2, -3, 0));
    }
}
