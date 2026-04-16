using System;

public class HealthSystem
{
    private int _maxHealth;
    private int _currentHealth;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;
    public bool IsAlive => _currentHealth > 0;

    public event Action<int, int> OnHealthChanged; //current and max
    public event Action OnDeath;


    public HealthSystem(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (!IsAlive) return;

        _currentHealth = Math.Max(_currentHealth - amount, 0);
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);

        if(_currentHealth <=0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        if (!IsAlive) return;

        _currentHealth = Math.Min(_currentHealth + amount, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
;    }
}
