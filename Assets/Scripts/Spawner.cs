using System;
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
            Enemy prefab = GetPrefabForType(config.Prefab,config.EnemyType);
            //prefab.SetTarget(config.Target);
            if (prefab!=null&& !_enemyPools.ContainsKey(config.EnemyType))
            {
                _enemyPools[config.EnemyType] = new GenericObjectPooL<Enemy>(prefab,config.InitialPoolSize);
            }
        }      
    }

    private Enemy GetPrefabForType(Enemy currentEnemy, EnemyType type)
    {       
            if (currentEnemy.Type == type)
            {
                return currentEnemy;
            }
        
        Debug.LogError($"No prefab found for enemy type: {type}");
        return null;
    }  

    private void StartAllSpawners()
    {
        if (_spawnCoroutines == null) return;

        for (int i = 0; i < _configSpawnPoints.Length; i++)
        {
            _spawnCoroutines[i] = StartCoroutine(SpawnFromPoint(_configSpawnPoints[i]));
        }
    }

    private IEnumerator SpawnFromPoint(SpawnPointConfig config)
    {
        if (!_enemyPools.ContainsKey(config.EnemyType))
        {
            Debug.LogError($"No pool found for enemy type: {config.EnemyType}");
            yield break;
        }

        GenericObjectPooL<Enemy> pool = _enemyPools[config.EnemyType];// здесь присваиваем одному пулу определенный тип
        WaitForSeconds wait = new WaitForSeconds(config.SpawnInterval);
     
            for (int i = 0; i < config.InitialSpawnSize; i++)
            {
                Enemy enemy = pool.GetObject();
                enemy.Died += OnEnemyDied;
                Debug.Log($"тип врага {enemy.Type} колличество спавна {config.InitialSpawnSize}");
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
    private void OnDestroy()
    {
        if (_spawnCoroutines != null)
        {
            foreach (var coroutine in _spawnCoroutines)
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
            }
        }
    }
}