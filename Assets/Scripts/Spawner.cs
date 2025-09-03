using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPointConfig[] _configSpawnPoints;

    private Dictionary<EnemyType, GenericObjectPooL<Enemy>> _enemyPools;
    private readonly List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

    private void Awake()
    {
        InitializePools();
        InitializeSpawnPoints();
    }

    private void Start()
    {
        StartAllSpawners();
    }

    private void InitializePools()
    {
        _enemyPools = new Dictionary<EnemyType, GenericObjectPooL<Enemy>>();

        foreach (var config in _configSpawnPoints)
        {
            if (!_enemyPools.ContainsKey(config.Prefab.Type))
            {
                _enemyPools[config.Prefab.Type] =
                    new GenericObjectPooL<Enemy>(config.Prefab, config.InitialPoolSize);
            }
        }
    }

    private void InitializeSpawnPoints()
    {
        foreach (var config in _configSpawnPoints)
        {
            GenericObjectPooL<Enemy> pool = _enemyPools[config.Prefab.Type];
            GameObject currentPoint = config.SpawnPoint.gameObject;
            SpawnPoint spawnPoint = currentPoint.GetComponent<SpawnPoint>();

            if (spawnPoint == null)
                spawnPoint = currentPoint.AddComponent<SpawnPoint>();

            spawnPoint.Init(config, pool);
            _spawnPoints.Add(spawnPoint);
        }
    }

    private void StartAllSpawners()
    {
        foreach (var spawnPoint in _spawnPoints)
            spawnPoint.StartSpawning();
    }
}
