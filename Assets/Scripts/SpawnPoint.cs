using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private SpawnPointConfig _config;
    private GenericObjectPooL<Enemy> _pool;
    private Coroutine _currentCoroutine;

    public void Init(SpawnPointConfig config, GenericObjectPooL<Enemy> pool)
    {
        _config = config;
        _pool = pool;
    }

    public void StartSpawning()
    {
        StopSpawning();
        _currentCoroutine = StartCoroutine(SpawnFromPoint());
    }

    public void StopSpawning()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
    }

    private IEnumerator SpawnFromPoint()
    {
        WaitForSeconds waitingTime = new WaitForSeconds(_config.SpawnInterval);

        for (int i = 0; i < _config.InitialSpawnSize; i++)
        {
            Enemy enemy = _pool.GetObject();
            enemy.Died += OnEnemyDied;

            enemy.SetTarget(_config.Target);
            enemy.transform.SetPositionAndRotation(_config.SpawnPoint.position, _config.SpawnPoint.rotation);

            yield return waitingTime;
        }
    }

    private void OnEnemyDied(Enemy enemy)
    {
        enemy.Died -= OnEnemyDied;
        _pool.ReturnObject(enemy);
    }

    private void OnDisable() => StopSpawning();
}
