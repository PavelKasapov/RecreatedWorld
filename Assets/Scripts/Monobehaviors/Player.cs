using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Player : MonoBehaviour, IPointerClickHandler
{
    public SpriteRenderer portrait;
    public WorldMapMovementController MovementController { get; private set; }
    public Vector3Int TargetGridCoords { get; private set; }
    public string Name { get; set; }
    public bool HasControl { get; set; }

    private GlobalStateManager _globalStateManager;
    private WorldMapMaganer _worldMapManager;
    private TileSelector _selector;

    [Inject]
    public void Construct(GlobalStateManager globalStateManager, WorldMapMaganer worldMapMaganer, TileSelector selector)
    {
        _globalStateManager = globalStateManager;
        _worldMapManager = worldMapMaganer;
        _selector = selector;
    }

    private void Awake()
    {
        MovementController = gameObject.GetComponent<WorldMapMovementController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject hitPlayer = eventData.pointerPressRaycast.gameObject;
        Player player = hitPlayer.GetComponent<Player>();
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                _selector.SelectPlayer(player);
                if (player.HasControl)
                {
                    _globalStateManager.ControlledPlayer = player;
                    _selector.spriteRenderer.color = Color.green;
                } else
                {
                    _selector.spriteRenderer.color = Color.red;
                }
                break;
            case PointerEventData.InputButton.Right:
                Debug.Log("Follow this character(not ready)");
                break;
        }
    }
}
