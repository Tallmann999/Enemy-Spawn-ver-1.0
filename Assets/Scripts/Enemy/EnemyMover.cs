using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 10f;

    private Rigidbody _rigidbody;
    private Transform _target;
    private Vector3 _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveToTarget();
        RotationToTarget();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void RotationToTarget()
    {
        _direction.y = 0;

        if (_direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_direction);
            _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime));
        }
    }

    private void MoveToTarget()
    {
        if (_target == null) return;

        _direction = (_target.position - transform.position).normalized;
        Vector3 movement = _direction * _movementSpeed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + movement);
    }
}
