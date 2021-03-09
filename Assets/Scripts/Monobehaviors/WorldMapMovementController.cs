using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class WorldMapMovementController : MonoBehaviour
{
    private float _speed = 1f;
    private Coroutine _moveCoroutine;
    private List<PathNode> _path;
    private Vector3 _nextNodeWorldCoords;
    private GlobalStateManager _globalStateManager;
    private PathfinderService _pathfinderService;
    private Grid _worldGrid;
    public Vector3Int TargetGridCoords { get; private set; }

    [Inject]
    public void Construct(GlobalStateManager globalStateManager, PathfinderService pathfinderService, Grid worldGrid)
    {
        _globalStateManager = globalStateManager;
        _pathfinderService = pathfinderService;
        _worldGrid = worldGrid;
    }

    public void SetMovePoint(Vector3Int targetGridCoords)
    {
        TargetGridCoords = targetGridCoords;
        Vector3Int gridCoords = _worldGrid.WorldToCell(gameObject.transform.position);
        if (gridCoords != targetGridCoords)
        {
            _path = _pathfinderService.FindPath(gridCoords, targetGridCoords);
            if (_moveCoroutine == null)
                _moveCoroutine = StartCoroutine(MoveRoutine());
        }
    }

    private IEnumerator MoveRoutine()
    {
        while (_path.Count > 0)
        {
            PathNode nextNode = _path.FirstOrDefault();
            _nextNodeWorldCoords = _worldGrid.GetCellCenterWorld(nextNode.Coords); 
            /* DrawLine to next tile */ Debug.DrawLine(transform.position, _nextNodeWorldCoords, Color.red, 2f); 
            while (transform.position != _nextNodeWorldCoords)
            {
                transform.position = Vector3.MoveTowards(transform.position, _nextNodeWorldCoords,
                    _speed * Time.deltaTime * (_globalStateManager.IsGlobalMapPaused ? 0 : 1) / nextNode.SelfCost);

                yield return null;
            }
            _path.Remove(nextNode);
            yield return null;
        }
        _moveCoroutine = null;
    }
   
}
