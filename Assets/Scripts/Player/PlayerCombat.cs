using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private WeaponData currentWeapon;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;

    private float _attackCooldownTimer = 0f;
    private PlayerInputActions _inputActions;

    public WeaponData CurrentWeapon => currentWeapon;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Gameplay.Enable();
        _inputActions.Gameplay.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        _inputActions.Gameplay.Attack.performed -= OnAttack;
        _inputActions.Gameplay.Disable();
    }

    private void Update()
    {
        if (_attackCooldownTimer > 0)
            _attackCooldownTimer -= Time.deltaTime;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (_attackCooldownTimer > 0 || currentWeapon == null) return;

        if (currentWeapon.weaponType == WeaponData.WeaponType.Melee)
        {
            MeleeAttack();
        }
        else
        {
            RangedAttack();
        }

        _attackCooldownTimer = currentWeapon.attackSpeed;
    }

    private void MeleeAttack()
    {
        // Detect all enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            currentWeapon.range,
            enemyLayers
        );

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent<IDamageable>(out var damageable))
            {
                Vector2 knockbackDir = (enemy.transform.position - transform.position).normalized;
                damageable.TakeDamage(currentWeapon.damage, knockbackDir);
            }
        }
    }

    private void RangedAttack()
    {
        // TODO: Spawn projectile (we'll add this later)
        Debug.Log("Ranged attack! (projectile coming soon)");
    }

    public void EquipWeapon(WeaponData newWeapon)
    {
        currentWeapon = newWeapon;
        Debug.Log($"Equipped {newWeapon.weaponName}!");
    }

    // Shows attack range in editor (yellow wire circle)
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, currentWeapon != null ? currentWeapon.range : 0.5f);
    }
}