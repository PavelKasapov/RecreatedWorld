using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer portrait;
    
    private float _speed = 1f;
    private Coroutine _moveCoroutine;
    private Grid _grid;
    private Vector3 _targetWorldCoords;
    private List<PathNode> _path;
    public Vector3Int TargetGridCoords { get; private set; }
    public string Name { get; set; }
    public bool DirectControl { get; set; }


    // Start is called before the first frame update
    void Awake()
    {
        _grid = WorldMapMaganer.Instance.GetComponent<Grid>();
    }

    public void SetMovePoint(Vector3Int targetGridCoords)
    {
        TargetGridCoords = targetGridCoords;
        Vector3Int gridCoords = WorldMapMaganer.Instance.Grid.WorldToCell(gameObject.transform.position);
        if (gridCoords != targetGridCoords)
        {
            _path = Pathfinder.FindPath(gridCoords, targetGridCoords);
            if (_moveCoroutine == null)
                _moveCoroutine = StartCoroutine(MoveRoutine());
            foreach (PathNode node in _path)
            {
                if (node.prevNode != null)
                {
                    Debug.DrawLine(WorldMapMaganer.Instance.Grid.GetCellCenterWorld(node.prevNode.Coords), WorldMapMaganer.Instance.Grid.GetCellCenterWorld(node.Coords), Color.red, 300f);
                }
            }
        }
    }


    private IEnumerator MoveRoutine()
    {
        while (_path.Count > 0)
        {
            PathNode nextNode = _path.FirstOrDefault();
            _targetWorldCoords = _grid.GetCellCenterWorld(nextNode.Coords);
            while (transform.position != _targetWorldCoords)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetWorldCoords,
                    _speed * Time.deltaTime * GlobalStateManager.Instance.GlobalMapTimeScale / nextNode.SelfCost);

                yield return null;
            }
            _path.Remove(nextNode);
            yield return null;
        }

        _moveCoroutine = null;
    }
}
