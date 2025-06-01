using System;

public interface IHealth
{
    public bool IsDead { get; }

    float CurrentHealth { get; }
    float MaxHealth { get; }

    event Action<float> OnHealthChanged;
    event Action OnDeath;

    public void ChangeHPBarEnable(bool state);
    void TakeDamage(float damage);
    void Heal(float amount);
}
