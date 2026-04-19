using UnityEngine;
public class ChaseState : EnemyState
{
    private Rigidbody2D _rb;
    public ChaseState(EnemyBase enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
        _rb = enemy.GetComponent<Rigidbody2D>();
    }
    public override void Update()
    {
        if (enemy.DistanceToPlayer() > enemy.Data.detectionRange)
        {
            stateMachine.ChangeState(new PatrolState(enemy, stateMachine));
            return;
        }
        if (enemy.DistanceToPlayer() < enemy.Data.attackRange)
        {
            stateMachine.ChangeState(new AttackState(enemy, stateMachine));
            return;
        }
        Vector2 dir = enemy.DirectionToPlayer();
        _rb.MovePosition(_rb.position + dir * enemy.Data.moveSpeed * Time.deltaTime);
    }
}
