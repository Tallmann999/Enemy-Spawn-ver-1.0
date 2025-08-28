using System;
using UnityEngine;

public class MonsterEnemy : Enemy
{
    //public MonsterEnemy(float lifeTime = 10) : base(7f) { }

    protected override void Awake()
    {
        base.Awake();
        _type = EnemyType.Monster;
        //_lifeTime = 4f;
    }
}
