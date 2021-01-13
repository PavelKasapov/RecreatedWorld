using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer portrait;
    
    private float _speed = 1f;
    private Coroutine _moveCoroutine;
    private Grid _grid;
    private Vector3 _targetWorldCoords;
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
        _targetWorldCoords = _grid.GetCellCenterWorld(targetGridCoords);

        if (_moveCoroutine == null)
            _moveCoroutine = StartCoroutine(MoveRoutine());
    }


    private IEnumerator MoveRoutine()
    {
        while (transform.position != _targetWorldCoords)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetWorldCoords,
                _speed * Time.deltaTime * GlobalStateManager.Instance.GlobalMapTimeScale);

            yield return null;
        }

        _moveCoroutine = null;
    }
}
