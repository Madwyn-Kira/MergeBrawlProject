using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour, IHealth
{
    [SerializeField] private HPBar healthBar;
    [SerializeField] private float maxHealth = 100f;

    public bool IsDead { get { return _isEntityDead; } }

    public float CurrentHealth { get; private set; }
    public float MaxHealth { get { return maxHealth; } }

    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    private bool _isEntityDead = false;

    public void Initialize(Slider hpBar, float _maxHealth)
    {
        maxHealth = _maxHealth;

        if (healthBar.HpBarSlider == null)
            healthBar.HpBarSlider = hpBar;

        healthBar.InitializeHPBar(maxHealth);
        CurrentHealth = maxHealth;
    }

    public void ChangeHPBarEnable(bool state)
    {
        healthBar.gameObject.SetActive(state);
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        healthBar.ChangeHPBarValue(-damage);
        OnHealthChanged?.Invoke(CurrentHealth);

        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0f, maxHealth);
        OnHealthChanged?.Invoke(CurrentHealth);
        healthBar?.ChangeHPBarValue(amount);
    }

    private void Die()
    {
        _isEntityDead = true;
        OnDeath?.Invoke();
    }
}
