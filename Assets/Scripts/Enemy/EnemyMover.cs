using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMover : MonoBehaviour
{
    private Transform _target;
    //private Rigidbody _rigidbody;
    private  float _movementSpeed;
    private Vector3 direction;
    protected bool _isMoving = false;
    private Enemy _currentEnemy;

}
