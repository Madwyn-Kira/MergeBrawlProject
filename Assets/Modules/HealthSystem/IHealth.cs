using System;

public interface IHealth
{
    public bool IsDead { get; }

    float CurrentHealth { get; }
    float MaxHealth { get; }

    event Action<float> OnHealthChanged;
    event Action OnDeath;

    void TakeDamage(float damage);
    void Heal(float amount);
}
