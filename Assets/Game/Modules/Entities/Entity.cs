using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class Entity : MonoBehaviour, IEntityEvents
{
    public StateMachine CurrentState;

    [HideInInspector]
    public NavMeshAgent NavAgent;
    [HideInInspector]
    public HeroConfig Config;
    [HideInInspector]
    public HeroEvolutionChainConfig EvolutionConfig;

    public BoardSpawnCell CurrentCell { get; private set; }

    public event Action OnMerge;
    public event Action<Entity> OnSpawned;
    public event Action<Entity> OnKilled;
    public event Action<Entity> OnDestroy;

    virtual public void InitializeParams()
    {
        NavAgent = GetComponent<NavMeshAgent>();

        ChangeState(new HeroMergePreparationState());
    }

    virtual public void Initialize(HeroConfig newConfig)
    {
        Config = newConfig;
        EvolutionConfig = Config.evolutionConfig;
        GetComponent<MeshRenderer>().material = newConfig.materialPrefab;
    }

    abstract public void Fight();

    virtual public void ChangeState(StateMachine newState)
    {
        if (CurrentState != null)
            CurrentState.ExitState();

        CurrentState = newState;
        CurrentState.EnterState(this);
    }

    public void AssignCell(BoardSpawnCell cell)
    {
        CurrentCell = cell;
    }

    public bool CanMergeWith(Entity other)
    {
        return other != null
               && other != this
               && other.Config.heroType == Config.heroType;
    }

    virtual public bool TryMerge(Entity other)
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

    virtual public void OnDie()
    {

    }
}
