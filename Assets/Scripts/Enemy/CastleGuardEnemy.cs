using UnityEngine;

public class CastleGuardEnemy : Enemy
{
    //public CastleGuardEnemy(float lifeTime = 10) : base(15f) { }

    protected override void Awake()
    {
        base.Awake();
        _type = EnemyType.CastleGuard;
        //_lifeTime = 12f;
    }
}
