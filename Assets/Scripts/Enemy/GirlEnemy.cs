using UnityEngine;

public class GirlEnemy : Enemy
{
    //public GirlEnemy(float lifeTime) : base(8f) { }

    protected override void Awake()
    {
        base.Awake();
        _type = EnemyType.Girl;
        //_lifeTime = 8f;
    }
}
