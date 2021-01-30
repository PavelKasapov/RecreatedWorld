using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IPointerClickHandler
{
    public SpriteRenderer portrait;

    private TileSelector _selector; 
    private float _speed = 1f;
    private Coroutine _moveCoroutine;
    private Vector3 _targetWorldCoords;
    private List<PathNode> _path;
    public Vector3Int TargetGridCoords { get; private set; }
    public string Name { get; set; }
    public bool DirectControl { get; set; }

    void Awake()
    {
        _selector = WorldMapMaganer.Instance.selector;
    }

    public void SetMovePoint(Vector3Int targetGridCoords)
    {
        TargetGridCoords = targetGridCoords;
        Vector3Int gridCoords = WorldMapMaganer.Instance.grid.WorldToCell(gameObject.transform.position);
        if (gridCoords != targetGridCoords)
        {
            _path = Pathfinder.FindPath(gridCoords, targetGridCoords);
            if (_moveCoroutine == null)
                _moveCoroutine = StartCoroutine(MoveRoutine());

            //// Full route DrawLine ////
            //foreach (PathNode node in _path)
            //{
            //    if (node.prevNode != null)
            //    {
            //        Debug.DrawLine(WorldMapMaganer.Instance.grid.GetCellCenterWorld(node.prevNode.Coords), WorldMapMaganer.Instance.grid.GetCellCenterWorld(node.Coords), Color.red, 2f);
            //    }
            //}
        }
    }

    private IEnumerator MoveRoutine()
    {
        while (_path.Count > 0)
        {
            PathNode nextNode = _path.FirstOrDefault();
            _targetWorldCoords = WorldMapMaganer.Instance.grid.GetCellCenterWorld(nextNode.Coords);
            //// Next tile DrawLine ////
            Debug.DrawLine(transform.position, _targetWorldCoords, Color.red, 2f);
            while (transform.position != _targetWorldCoords)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetWorldCoords,
                    _speed * Time.deltaTime * (GlobalStateManager.Instance.IsGlobalMapPaused? 0 : 1) / nextNode.SelfCost);

                yield return null;
            }
            _path.Remove(nextNode);
            yield return null;
        }

        _moveCoroutine = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 worldCoord = Camera.main.ScreenToWorldPoint(eventData.position);
        Vector3Int gridCoord = WorldMapMaganer.Instance.grid.WorldToCell(worldCoord);
        GameObject hitPlayer = eventData.pointerPressRaycast.gameObject;
        Player player = hitPlayer.GetComponent<Player>();
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                WorldMapMaganer.Instance.selector.transform.parent = hitPlayer.transform;
                WorldMapMaganer.Instance.selector.transform.position = hitPlayer.transform.position ;
                WorldMapMaganer.Instance.selector.spriteRenderer.sprite = WorldMapMaganer.Instance.selector.characterSelectorSprite;
                if (player.DirectControl)
                {
                    GlobalStateManager.Instance.ControlledPlayer = player;
                    WorldMapMaganer.Instance.selector.spriteRenderer.color = Color.green;
                } else
                {
                    WorldMapMaganer.Instance.selector.spriteRenderer.color = Color.red;
                }
                break;
            case PointerEventData.InputButton.Right:
                Debug.Log("Follow this character(not ready)");
                break;
        }
    }
}
