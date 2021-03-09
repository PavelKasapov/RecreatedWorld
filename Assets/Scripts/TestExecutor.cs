using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class TestExecutor : MonoBehaviour
{
    public TileSelector tileSelector;
    public Grid grid;
    public Controls controls;

    void Start()
    {
        Debug.Log("Testing");

        //WorldMapMaganer.Instance.GenerateMap(40, 40);
        //CharacterManager.Instance.SpawnPlayer("Gector", true, "Sprites/PortraitSample", new Vector3(0, 0, 0), new Vector3Int(5, 5, 0));
        //GlobalStateManager.Instance.IsGlobalMapPaused = false;
        //StartCoroutine(TestSaveAndLoadRoutine());
        //StartCoroutine(TestGoAroundRoutine());
        //List<PathNode> path = Pathfinder.FindPath(new Vector3Int(0, 0, 0), new Vector3Int(5, 5, 0));
        //GlobalStateManager.Instance.ControlledPlayer.SetMovePoint(new Vector3Int(2, 2, 0));
        //EventSystem.current.currentInputModule.
    }

    //private IEnumerator TestSaveAndLoadRoutine()
    //{
    //    yield return new WaitForSeconds(1);
    //    GlobalStateManager.Instance.SaveGame();
    //    Debug.Log("Save");
    //    yield return new WaitForSeconds(1);
    //    GlobalStateManager.Instance.LoadGame();
    //    Debug.Log("Load");
    //    GlobalStateManager.Instance.IsGlobalMapPaused = false;
    //    Debug.Log("Unpause");
    //    yield return new WaitForSeconds(2.5f);
    //    GlobalStateManager.Instance.LoadGame();
    //    Debug.Log("Load");
    //}

    //private IEnumerator TestGoAroundRoutine()
    //{
    //    yield return new WaitForSeconds(2);
    //    GlobalStateManager.Instance.ControlledPlayer.SetMovePoint(new Vector3Int(2, 2, 0));
    //    yield return new WaitForSeconds(2);
    //    //GlobalStateManager.Instance.ControlledPlayer.SetMovePoint(new Vector3Int(-2, -3, 0));
    //}

    public void onClickAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log(Mouse.current.position.ReadValue());
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit);
                Transform objectHit = hit.transform;

                // Do something with the object that was hit by the raycast.
            }
        }
    }
   

}
