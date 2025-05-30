using UnityEngine;

public abstract class EnemyController : Entity
{
    [SerializeField]
    private WeaponController Weapon;

    private EnemyConfig _enemySettings;
    public override WeaponController WeaponController { get { return Weapon; } }

    private void Start()
    {
        base.InitializeParams();
    }

    override public void Initialize<T>(T newConfig)
    {

        _enemySettings = newConfig as EnemyConfig;

        base._healthController.Initialize(null, _enemySettings.MaxHealth);
        base._healthController.OnHealthChanged += ChangeHealth;
        base._healthController.OnDeath += Die;

        Weapon.Initialize();

        //ChangeState(new EnemyIdleState());
    }

    private void ChangeHealth(float amount)
    {

    }

    private void Die()
    {
        //ChangeState(new EnemyDieState());
    }

    override public void OnDestroyEntity()
    {
        base.OnDestroyEntity();

        base._healthController.OnHealthChanged -= ChangeHealth;
        base._healthController.OnDeath -= Die;
    }
}
