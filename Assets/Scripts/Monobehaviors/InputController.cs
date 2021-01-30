using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public CameraController cameraController;
    public bool isEdgeScrollEnable = true;
    
    public void OnCameraMovementInput(InputAction.CallbackContext context)
    {
        Vector2 moveCamDirection = context.ReadValue<Vector2>();
        if (moveCamDirection != Vector2.zero)
        {
            isEdgeScrollEnable = false;
        }
        else
        {
            isEdgeScrollEnable = true;
        }
        cameraController.MoveCam(moveCamDirection);
    }

    public void OnPointerMovement(InputAction.CallbackContext context)
    {
        if (isEdgeScrollEnable)
        {
            Vector2 pointerPos = context.ReadValue<Vector2>();
            Vector2 moveCamDirection = ScreenPosToEdgeDirection(pointerPos);
            cameraController.MoveCam(moveCamDirection);
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
                if (WorldMapMaganer.Instance.WorldMap.Find(tile => tile.Coords == gridCoord).TerrainType != TerrainType.Water)
                {
                    GlobalStateManager.Instance.ControlledPlayer?.SetMovePoint(gridCoord);
                }
                break;
        }
    }


    private Vector2 ScreenPosToEdgeDirection(Vector2 screenPos)
    {
        Vector2 edgeDirection = new Vector2();
        switch (screenPos.x)
        {
            case float n when (n > 0 && n < Screen.width):
                {
                    edgeDirection.x = 0;
                    break;
                }
            case float n when (n <= 0):
                {
                    edgeDirection.x = -1;
                    break;
                }
            
            case float n when (n >= Screen.width):
                {
                    edgeDirection.x = 1;
                    break;
                }
        }
        switch (screenPos.y)
        {
            case float n when (n > 0 && n < Screen.height):
                {
                    edgeDirection.y = 0;
                    break;
                }
            case float n when (n <= 0):
                {
                    edgeDirection.y = -1;
                    break;
                }

            case float n when (n >= Screen.height):
                {
                    edgeDirection.y = 1;
                    break;
                }
        }
        return edgeDirection.normalized;
    }

}
