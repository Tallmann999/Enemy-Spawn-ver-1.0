using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType _type;
    [SerializeField] private float _lifeTime = 10f;

    private EnemyMover _enemyMover;
    private Coroutine _currentCoroutine;
    private WaitForSeconds _currentWaitForSeconds;

    public EnemyType Type => _type;
    public event Action<Enemy> Died;

    private void Awake()
    {
        _currentWaitForSeconds = new WaitForSeconds(_lifeTime);
        _enemyMover = GetComponent<EnemyMover>();
    }

    private void Start()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(LifeTimer());
        }

        _currentCoroutine = StartCoroutine(LifeTimer());
    }

    public void SetTarget(Target target)
    {
        _enemyMover.SetTarget(target.transform);
    }

    private IEnumerator LifeTimer()
    {
        yield return _currentWaitForSeconds;
        Died?.Invoke(this);
    }
}