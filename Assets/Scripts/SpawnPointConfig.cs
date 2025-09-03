using UnityEngine;

[System.Serializable]
public class SpawnPointConfig
{
    public Transform SpawnPoint;     
    public Enemy Prefab;
    public Target Target;
    public float SpawnInterval = 2f;  
    public int InitialPoolSize = 10;  
    public int InitialSpawnSize = 15;  
}