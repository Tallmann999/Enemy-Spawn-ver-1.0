using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 10f;
    private Enemy _enemy;

    private Rigidbody _rigidbody;
    private Vector3 _target;

    //private Vector3 direction;
    protected bool _isMoving = false;

    private void OnEnable()
    {
        _enemy.TargetDirection += OnGetTarget;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _enemy = GetComponent<Enemy>();
    }

    private void FixedUpdate()
    {
        StartCoroutine(MoveCoroutine());

    }


    //private void MoveTowardsTarget()
    //{
    //    // Рассчитываем направление к цели
    //    Vector3 direction = (_target - transform.position).normalized;

    //    // Двигаем Rigidbody
    //    Vector3 movement = direction * _movementSpeed * Time.fixedDeltaTime;
    //    _rigidbody.MovePosition(_rigidbody.position + movement);

    //    // Для отладки
    //    //Debug.DrawLine(transform.position, _target.position, Color.red);
    //}

    //private void RotateTowardsTarget()
    //{
    //    if (_target == null) return;

    //    // Рассчитываем направление к цели
    //    Vector3 direction = (Vector3.right - transform.position).normalized;

    //    // Игнорируем вертикальную составляющую для поворота
    //    direction.y = 0;

    //    if (direction != Vector3.zero)
    //    {
    //        Quaternion targetRotation = Quaternion.LookRotation(direction);

    //        // Плавно поворачиваем Rigidbody
    //        _rigidbody.MoveRotation(Quaternion.Slerp(
    //            _rigidbody.rotation,
    //            targetRotation,
    //            _rotationSpeed * Time.fixedDeltaTime
    //        ));
    //    }
    //}

    private void OnGetTarget(Target target)
    {
        _target = target.transform.position;
        _isMoving = target != null;

        // Запускаем корутину для постоянного движения
        //if (_isMoving)
        //{
        //    StartCoroutine(MoveCoroutine());
        //    Debug.Log("корутина запустилась");
        //}
    }
    // Для отладки
    private void Update()
    {
        if (_target != null )
        {
            Debug.DrawLine(transform.position, _target, Color.green);
        }
    }

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            Vector3 targetPosition = new Vector3(_target.x, _target.y, _target.z);
            Vector3 direction = (targetPosition - transform.position).normalized;

            Vector3 movement = direction * _movementSpeed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(_rigidbody.position + movement);
            RotateTowardsTarget(direction);

            yield return new WaitForFixedUpdate();
        }
    }

    private void RotateTowardsTarget(Vector3 direction)
    {
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime
            ));
        }
    }

    private void OnDisable()
    {
        _enemy.TargetDirection -= OnGetTarget;
        // не работает подписка 
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Для отладки в редакторе
    private void OnDrawGizmos()
    {
        if (_target != null && _isMoving)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, _target);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_target, 0.5f);
        }
    }
}
