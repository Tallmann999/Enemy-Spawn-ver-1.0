using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPointConfig[] _configSpawnPoints;

    private Coroutine[] _spawnCoroutines;
    private Dictionary<EnemyType, GenericObjectPooL<Enemy>> _enemyPools;

    private void Awake()
    {
        InitializePools();
    }

    private void Start()
    {

        StartAllSpawners();
    }

    private void InitializePools()
    {
        _enemyPools = new Dictionary<EnemyType, GenericObjectPooL<Enemy>>();
        _spawnCoroutines = new Coroutine[_configSpawnPoints.Length];

        foreach (var config in _configSpawnPoints)
        {
            Enemy prefab = GetPrefabForType(config.Prefab, config.EnemyType);

            if (prefab != null && !_enemyPools.ContainsKey(config.EnemyType))
            {
                _enemyPools[config.EnemyType] = new GenericObjectPooL<Enemy>(prefab, config.InitialPoolSize);
            }
        }
    }

    private Enemy GetPrefabForType(Enemy currentEnemy, EnemyType type)
    {
        if (currentEnemy.Type == type)
        {
            return currentEnemy;
        }

        return null;
    }

    private void StartAllSpawners()
    {     
        for (int i = 0; i < _configSpawnPoints.Length; i++)
        {
            if (_spawnCoroutines[i]!= null)
            {
                StopCoroutine(_spawnCoroutines[i]);
            }

            _spawnCoroutines[i] = StartCoroutine(SpawnFromPoint(_configSpawnPoints[i]));
        }
    }

    private IEnumerator SpawnFromPoint(SpawnPointConfig config)
    {      
        GenericObjectPooL<Enemy> pool = _enemyPools[config.EnemyType];
        WaitForSeconds wait = new WaitForSeconds(config.SpawnInterval);

        for (int i = 0; i < config.InitialSpawnSize; i++)
        {
            Enemy enemy = pool.GetObject();
            enemy.Died += OnEnemyDied;
            enemy.SetTarget(config.Target); 
            enemy.transform.position = config.SpawnPoint.position;
            enemy.transform.rotation = config.SpawnPoint.rotation;

            yield return wait;
        }
    }

    private void OnEnemyDied(Enemy enemy)
    {
        if (_enemyPools.ContainsKey(enemy.Type))
        {
            enemy.Died -= OnEnemyDied;
            _enemyPools[enemy.Type].ReturnObject(enemy);
        }
    }
}