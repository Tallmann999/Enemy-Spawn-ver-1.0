using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform _pathTarget;
    [SerializeField] private float _movingSpeed;
    private int _currentPointIndex;
    private bool _isMoving;
    private Transform[] _points;


    private void Awake()
    {
        InitPoints();
    }

    private void Update()
    {
        MoveToPoints();
    }

    private void MoveToPoints()
    {
        Transform target = _points[_currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, _movingSpeed * Time.deltaTime);
        _isMoving = true;

        if (transform.position == target.position)
        {
            _currentPointIndex++;
        }

        if (_currentPointIndex >= _points.Length)
        {
            _currentPointIndex = 0;
        }
    }
    private void InitPoints()
    {
        _points = new Transform[_pathTarget.childCount];
        for (int i = 0; i < _pathTarget.childCount; i++)
        {
            _points[i] = _pathTarget.GetChild(i);
        }
    }
}
