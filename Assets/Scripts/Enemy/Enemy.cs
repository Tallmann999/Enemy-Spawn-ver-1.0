using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyType _type;
    protected Coroutine _currentCoroutine;
    protected WaitForSeconds _currentWaitForSeconds;
    protected float _lifeTime = 8f;
    public EnemyType Type => _type;
    public event Action<Enemy> Died;
   
    protected virtual void Awake()
    {
        _currentWaitForSeconds = new WaitForSeconds(_lifeTime);
    }
   

    protected void Start()
    {

        if (_currentCoroutine != null)
        {
            StopCoroutine(LifeTimer());
        }

        _currentCoroutine = StartCoroutine(LifeTimer());
    }

    protected virtual IEnumerator LifeTimer()
    {
        yield return _currentWaitForSeconds;
        Died?.Invoke(this);
    }
}