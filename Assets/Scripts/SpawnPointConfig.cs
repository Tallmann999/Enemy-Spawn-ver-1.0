using UnityEngine;

[System.Serializable]
public class SpawnPointConfig
{
    public Transform SpawnPoint;     
    public EnemyType EnemyType;    
    
    //public MovingTarget MovingTarget;   !!!! �������� �� ����� ���� ������� ��� �������
    //���������� ����� ����� ���� ���� ���������

    public float SpawnInterval = 2f;  
    //public float EnemySpeed = 5f;    
    public int InitialPoolSize = 10;  
    public int InitialSpawnSize = 15;  
}