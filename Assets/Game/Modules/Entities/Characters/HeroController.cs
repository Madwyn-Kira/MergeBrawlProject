using UnityEngine;

public abstract class HeroController : Entity
{
    [SerializeField]
    private WeaponController Weapon;
    public override WeaponController WeaponController { get { return Weapon; } }

    [HideInInspector]
    public HeroConfig Config;
    [HideInInspector]
    public HeroEvolutionChainConfig EvolutionConfig;

    public override void InitializeParams()
    {
        base.InitializeParams();

        ChangeState(new HeroMergePreparationState());
    }

    override public void Initialize<T>(T newConfig)
    {
        Config = newConfig as HeroConfig;
        EvolutionConfig = Config.evolutionConfig;
        GetComponent<MeshRenderer>().material = Config.materialPrefab;

        base._healthController.Initialize(null, Config.baseHealth);
        base._healthController.OnHealthChanged += ChangeHealth;
        base._healthController.OnDeath += Die;

        Weapon.Initialize();

        //ChangeState(new EnemyIdleState());
    }
    public bool CanMergeWith(HeroController other)
    {
        return other != null
               && other != this
               && other.Config.heroType == Config.heroType;
    }

    virtual public bool TryMerge(HeroController other)
    {
        if (!CanMergeWith(other))
            return false;

        int _currentConfigIndexInEvolution = EvolutionConfig.EvolutionChain.IndexOf(Config);
        if (EvolutionConfig.EvolutionChain.IndexOf(Config) < EvolutionConfig.EvolutionChain.Count - 1)
        {
            HeroConfig nextData = EvolutionConfig.EvolutionChain[_currentConfigIndexInEvolution + 1];
            Initialize(nextData);
            Destroy(other.gameObject);
        }

        return true;
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
