using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Coroutine _currentCoroutine;
    private Vector3 _movementDirection;
    private WaitForSeconds _currentWaitForSeconds;
    private float _lifeTime = 8f;
    private float _movementSpeed;
    private bool _isMoving = false;

    public event Action<Enemy> Died;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _currentWaitForSeconds = new WaitForSeconds(_lifeTime);
    }

    private void Update()
    {
        if (_isMoving)
        {
            Move();

            if (_currentCoroutine != null)
            {
                StopCoroutine(LifeTimer());
            }

            _currentCoroutine = StartCoroutine(LifeTimer());
        }
    }

    public void InitializeMovement(Vector3 direction, float speed)
    {
        _movementDirection = direction.normalized;
        _movementSpeed = speed;
        _isMoving = true;
    }

    private void Move()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.AddForce(_movementDirection.normalized * _movementSpeed, ForceMode.VelocityChange);
    }

    private IEnumerator LifeTimer()
    {
        yield return _currentWaitForSeconds;
        Died?.Invoke(this);
    }
}