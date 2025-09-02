using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyType _type;
    [SerializeField] protected float _lifeTime = 20f;
    protected Coroutine _currentCoroutine;
    protected WaitForSeconds _currentWaitForSeconds;
    public EnemyType Type => _type;
    public event Action<Enemy> Died;
    public event Action<Target> TargetDirection;

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

    public void SetTarget(Target target)
    {
        Debug.Log($"Setting target for {gameObject.name}: {(target != null ? target.name : "null")}");
        TargetDirection?.Invoke(target);
    }
}