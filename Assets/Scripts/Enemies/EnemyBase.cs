using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected EnemyData data;

    protected HealthSystem health;
    protected StateMachine stateMachine;
    protected Transform playerTransform;

    public EnemyData Data => data;
    public Transform PlayerTransform => playerTransform;

    public bool IsAlive => health.IsAlive;

    protected virtual void Awake()
    {
        health = new HealthSystem(data.maxHealth);
        stateMachine = new StateMachine();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    protected virtual void Update()
    {
        stateMachine?.Update();
    }

    public virtual void Die()
    {
        Debug.Log($"{data.enemyName} is dead");
        Destroy(gameObject);
    }

    public virtual void TakeDamage(int amount, Vector2 knockbackDirection)
    {
        if (!IsAlive) return;
        health.TakeDamage(amount);
        Debug.Log($"{data.enemyName} took {amount} damage! HP: {health.CurrentHealth}/{health.MaxHealth}");
        if (!IsAlive)
        {
            Die();
        }
    }

    public float DistanceToPlayer()
    {
        if (playerTransform == null) return float.MaxValue;
        return Vector2.Distance(transform.position, playerTransform.position);
    }

    public Vector2 DirectionToPlayer()
    {
        if (playerTransform == null) return Vector2.zero;
        return (Vector2)(playerTransform.position - transform.position).normalized;
    }
}
