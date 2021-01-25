using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public void TestClick(InputAction.CallbackContext context)
    {
        
        if(context.performed)
        {
            //PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            //Debug.Log(pointerEventData);
        }
        
    }
    public void OnGlobalMapMouseClick(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = (PointerEventData)baseEventData;
        Vector3 worldCoord = Camera.main.ScreenToWorldPoint(pointerEventData.position);
        Vector3Int gridCoord = WorldMapMaganer.Instance.grid.WorldToCell(worldCoord);
        switch (pointerEventData.button)
        {
            case PointerEventData.InputButton.Left:
                WorldMapMaganer.Instance.selector.transform.parent = null;
                WorldMapMaganer.Instance.selector.SelectTile(gridCoord);
                WorldMapMaganer.Instance.selector.spriteRenderer.sprite = WorldMapMaganer.Instance.selector.tileSelectorSprite;
                GlobalStateManager.Instance.ControlledPlayer = null;
                break;
            case PointerEventData.InputButton.Right:
                GlobalStateManager.Instance.ControlledPlayer?.SetMovePoint(gridCoord);
                break;
        }
    }
    public void OnCharacterMouseClick(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = (PointerEventData)baseEventData;
        Vector3 worldCoord = Camera.main.ScreenToWorldPoint(pointerEventData.position);
        Vector3Int gridCoord = WorldMapMaganer.Instance.grid.WorldToCell(worldCoord);
        GameObject hitPlayer = pointerEventData.pointerPressRaycast.gameObject;
        switch (pointerEventData.button)
        {
            case PointerEventData.InputButton.Left:
                WorldMapMaganer.Instance.selector.transform.parent = hitPlayer.transform;
                WorldMapMaganer.Instance.selector.transform.position = Vector3.zero;
                break;
            case PointerEventData.InputButton.Right:
                //TODO remove this test line
                GlobalStateManager.Instance.ControlledPlayer = hitPlayer.GetComponent<Player>();
                break;
        }
    }



}
