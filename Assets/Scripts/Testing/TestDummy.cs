using UnityEngine;

/// <summary>
/// Temporary test target. Implements IDamageable so PlayerCombat can hit it.
/// Delete this script once real enemies are working.
/// </summary>
public class TestDummy : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 20;
    private HealthSystem _health;
    private SpriteRenderer _spriteRenderer;

    public bool IsAlive => _health.IsAlive;

    private void Awake()
    {
        _health = new HealthSystem(maxHealth);
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _health.OnHealthChanged += (current, max) =>
        {
            Debug.Log($"[TestDummy] HP: {current}/{max}");
        };

        _health.OnDeath += () =>
        {
            Debug.Log("[TestDummy] I'M DEAD! Combat works!");
            _spriteRenderer.color = Color.gray;
        };
    }

    public void TakeDamage(int amount, Vector2 knockbackDirection)
    {
        if (!IsAlive) return;

        _health.TakeDamage(amount);

        // Flash red briefly
        StartCoroutine(FlashRed());

        // Log for testing
        Debug.Log($"[TestDummy] Took {amount} damage! Knockback: {knockbackDirection}");
    }

    public void Die()
    {
        Debug.Log("[TestDummy] Die() called.");
    }

    private System.Collections.IEnumerator FlashRed()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        if (IsAlive)
            _spriteRenderer.color = Color.white;
        else
            _spriteRenderer.color = Color.gray;
    }
}
