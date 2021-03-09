using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

public class InputController : MonoBehaviour
{
    public CameraController cameraController;
    public bool isEdgeScrollEnable = true;

    private GlobalStateManager _globalStateManager;
    private WorldMapMaganer _worldMapManager;
    private Grid _worldGrid;
    private TileSelector _selector;

    [Inject]
    public void Construct(GlobalStateManager globalStateManager, WorldMapMaganer worldMapMaganer, Grid worldGrid, TileSelector selector)
    {
        _globalStateManager = globalStateManager;
        _worldMapManager = worldMapMaganer;
        _worldGrid = worldGrid;
        _selector = selector;
    }

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
        Vector3Int gridCoord = _worldGrid.WorldToCell(worldCoord);
        switch (pointerEventData.button)
        {
            case PointerEventData.InputButton.Left:
                _selector.SelectTile(gridCoord);
                _globalStateManager.ControlledPlayer = null;
                break;
            case PointerEventData.InputButton.Right:
                if (_worldMapManager.WorldMap.Find(tile => tile.Coords == gridCoord).TerrainType != TerrainType.Water)
                {
                    _globalStateManager.ControlledPlayer?.MovementController.SetMovePoint(gridCoord);
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
