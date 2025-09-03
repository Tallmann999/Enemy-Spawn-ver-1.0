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
        T obj;

        if (_pool.Count == 0)
        {
            obj = GameObject.Instantiate(_prefab, Vector3.zero, Quaternion.identity);
        }
        else
        {
            obj = _pool.Dequeue();
        }

        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnObject(T poolObject)
    {
        poolObject.gameObject.SetActive(false);
        _pool.Enqueue(poolObject);
    }
}