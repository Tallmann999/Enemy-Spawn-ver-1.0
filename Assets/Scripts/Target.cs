using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform _pathTarget;
    [SerializeField] private float _movingSpeed;
    [SerializeField] private Transform[] _points;

    private int _currentPointIndex = 0;

    private void Update()
    {
        MoveToPoints();
    }

    [ContextMenu("Refresh Child Array")]
    private void InitPoints()
    {
        _points = new Transform[_pathTarget.childCount];

        for (int i = 0; i < _pathTarget.childCount; i++)
        {
            _points[i] = _pathTarget.GetChild(i);
        }
    }

    private void MoveToPoints()
    {
        Transform target = _points[_currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, _movingSpeed * Time.deltaTime);

        if (transform.position == target.position)
            _currentPointIndex = ++_currentPointIndex % _points.Length;
    }
}
