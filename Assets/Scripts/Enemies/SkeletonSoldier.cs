using UnityEngine;
public class SkeletonSoldier : EnemyBase
{
    protected override void Awake()
    {
        base.Awake();
        stateMachine.ChangeState(new PatrolState(this, stateMachine));
    }
}
