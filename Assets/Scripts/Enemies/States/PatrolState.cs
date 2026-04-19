using UnityEngine;
public class PatrolState : EnemyState
{
    private float _waitTimer;
    private Vector2 _targetPosition;
    private Rigidbody2D _rb;
    public PatrolState(EnemyBase enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
        _rb = enemy.GetComponent<Rigidbody2D>();
    }
    public override void Enter()
    {
        _targetPosition = (Vector2)enemy.transform.position + Random.insideUnitCircle * 3f;
    }
    public override void Update()
    {
        if (enemy.DistanceToPlayer() < enemy.Data.detectionRange)
        {
            stateMachine.ChangeState(new ChaseState(enemy, stateMachine));
            return;
        }
        if (_waitTimer > 0)
        {
            _waitTimer -= Time.deltaTime;
        }
        else
        {
            Vector2 newPos = Vector2.MoveTowards(_rb.position, _targetPosition, enemy.Data.moveSpeed * Time.deltaTime);
            _rb.MovePosition(newPos);
            if (Vector2.Distance(enemy.transform.position, _targetPosition) < 0.1f)
            {
                _waitTimer = 2f;
                _targetPosition = (Vector2)enemy.transform.position + Random.insideUnitCircle * 3f;
            }
        }
    }
}
