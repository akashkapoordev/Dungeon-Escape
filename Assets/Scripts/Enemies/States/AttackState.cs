using UnityEngine;
public class AttackState : EnemyState
{
    private Rigidbody2D _rb;
    private float coolDownTimer;
    private bool hasAttacked = false;
    public AttackState(EnemyBase enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
        _rb = enemy.GetComponent<Rigidbody2D>();
    }
    public override void Update()
    {
        if (enemy == null || enemy.PlayerTransform == null || enemy.Data == null)
        {
            return;
        }
        if (enemy.DistanceToPlayer() > enemy.Data.attackRange)
        {
            stateMachine.ChangeState(new ChaseState(enemy, stateMachine));
            return;
        }
        if (!hasAttacked)
        {
            hasAttacked = true;
            var playerTransform = enemy.PlayerTransform;
            if (playerTransform != null)
            {
                IDamageable player = playerTransform.GetComponent<IDamageable>();
                if (player != null)
                {
                    player.TakeDamage(enemy.Data.damage, enemy.DirectionToPlayer());
                }
            }
            coolDownTimer = enemy.Data.attackCooldown;
        }
        else
        {
            coolDownTimer -= Time.deltaTime;
            if (coolDownTimer <= 0f)
            {
                hasAttacked = false;
                coolDownTimer = 0f;
            }
        }
    }
}
