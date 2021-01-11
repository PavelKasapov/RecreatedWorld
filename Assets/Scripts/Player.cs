using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer portrait;
    //public Vector3Int Coords { get; private set; }
    public string Name { get;  set; }
    private Vector3 _targetWorldCoords;
    private float _speed = 1f;
    private Coroutine _moveCoroutine;
    private Grid _grid;
    private TileSelector _selector;

    
    // Start is called before the first frame update
    void Awake()
    {
        _grid = WorldMapMaganer.Instance.GetComponent<Grid>();
        _selector = Object.FindObjectOfType<TileSelector>();
    }

    public void Move()
    {
        _targetWorldCoords = _grid.GetCellCenterWorld(_selector.SelectedTile.Coords);

        if (_moveCoroutine == null)
            _moveCoroutine = StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (transform.position != _targetWorldCoords)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetWorldCoords, _speed * Time.deltaTime);

            yield return null;
        }

        _moveCoroutine = null;
    }
}
