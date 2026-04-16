using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int amount, Vector2 knockbackDirection);
    void Die();
    bool IsAAlive { get; }
}
