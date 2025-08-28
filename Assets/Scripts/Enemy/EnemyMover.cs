using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMover : MonoBehaviour
{
    private Transform _target;
    private Rigidbody _rigidbody;
    private  float _movementSpeed;

    protected bool _isMoving = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    //public void Initialize()
    //{
    //    //_movementDirection = target;
    //    //_movementSpeed = speed;
    //    _isMoving = true;
    //    //HasMove?.Invoke(_isMoving);
    //}

    private void Update()
    {
        //if (transform.position == _target.position)
        //{
        //}
    }
}
