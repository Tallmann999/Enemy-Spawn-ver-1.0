using UnityEngine;
using System.Collections.Generic;

public class GenericObjectPooL<T> where T : Enemy
{
    private T _prefab;
    private Queue<T> _pool = new Queue<T>();

    public GenericObjectPooL(T prefab, int initializeSize)
    {
        _prefab = prefab;

        for (int i = 0; i < initializeSize; i++)
        {
            T newObject = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newObject.gameObject.SetActive(false);
            _pool.Enqueue(newObject);
        }
    }

    public T GetObject()
    {
        if (_pool.Count == 0)
        {
            T obj = GameObject.Instantiate(_prefab, Vector3.zero, Quaternion.identity);
            return obj;
        }

        T pooledObj = _pool.Dequeue();
        pooledObj.gameObject.SetActive(true);
        return pooledObj;
    }

    public void ReturnObject(T poolObject)
    {
        _pool.Enqueue(poolObject);
        poolObject.gameObject.SetActive(false);
    }
}