using System.Collections;
using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    private GlobalStateManager _globalStateManager;
    private Coroutine _moveCamCoroutine;
    private float _speed = 5f;
    private Vector2 _moveDirection;

    [Inject]
    public void Construct(GlobalStateManager globalStateManager)
    {
        _globalStateManager = globalStateManager;
    }

    public void MoveCam(Vector2 direction)
    {
        if (direction != _moveDirection)
        {
            _moveDirection = direction;

            if (_moveCamCoroutine == null)
            {
                _moveCamCoroutine = StartCoroutine(MoveRoutine());
            }
        }
        
    }
    private IEnumerator MoveRoutine()
    {
        while (_moveDirection != Vector2.zero && Application.isFocused && !_globalStateManager.IsMenuOpened)
        {

            transform.position += (Vector3)_moveDirection * _speed * Time.deltaTime;

            yield return null;
            
        }
        _moveCamCoroutine = null;
    }
}
