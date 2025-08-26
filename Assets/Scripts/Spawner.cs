using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Enemy _prefab;
    [SerializeField] private float _enemyMoveSpeed;

    private Vector3 _movementDirection = Vector3.forward;
    private GenericObjectPooL<Enemy> _enemyPool;
    private int _enemyPoolSize = 15;
    private int _enemySpawnSize = 10;
    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _enemyPool = new GenericObjectPooL<Enemy>(_prefab, _enemyPoolSize);
    }

    private void Start()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(ÑreatingEnemyAtRandomPoint());
        }

        _currentCoroutine = StartCoroutine(ÑreatingEnemyAtRandomPoint());
    }

    private IEnumerator ÑreatingEnemyAtRandomPoint()
    {
        for (int i = 0; i < _enemySpawnSize; i++)
        {
            Enemy enemy = _enemyPool.GetObject();
            enemy.Died += OnReturnPoolObject;
            Transform spawnPoint = _points[Random.Range(0, _points.Length)];
            enemy.transform.position = spawnPoint.position;
            enemy.InitializeMovement(_movementDirection, _enemyMoveSpeed);
            yield return new WaitForSeconds(2f);
        }
    }

    private void OnReturnPoolObject(Enemy currentEnemy)
    {
        currentEnemy.Died -= OnReturnPoolObject;
        _enemyPool.ReturnObject(currentEnemy);
    }
}
