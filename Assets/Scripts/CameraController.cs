using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Coroutine _moveCamCoroutine;
    private float _speed = 5f;
    private Vector2 _moveDirection;

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
        while (_moveDirection != Vector2.zero)
        {

            transform.position += (Vector3)_moveDirection * _speed * Time.deltaTime;

            yield return null;
            
        }
        _moveCamCoroutine = null;
    }
}
