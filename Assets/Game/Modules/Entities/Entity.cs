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

    [HideInInspector]
    public BoardSpawnCell currentCell;

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

    public void ChangeCell(BoardSpawnCell cell)
    {
        if (currentCell != null)
            currentCell.ClearCell();

        currentCell = cell;
        currentCell.PlaceHero(this);
    }

    abstract public void Fight();

    virtual public void ChangeState(StateMachine newState)
    {
        if (CurrentState != null)
            CurrentState.ExitState();

        CurrentState = newState;
        CurrentState.EnterState(this);
    }

    virtual public bool Merge(Entity other)
    {
        if (other.Config == Config)
        {
            int _currentConfigIndexInEvolution = EvolutionConfig.EvolutionChain.IndexOf(Config);
            if (EvolutionConfig.EvolutionChain.IndexOf(Config) < EvolutionConfig.EvolutionChain.Count - 1)
            {
                HeroConfig nextData = EvolutionConfig.EvolutionChain[_currentConfigIndexInEvolution + 1];
                Initialize(nextData);
                Destroy(other.gameObject);

                return true;
            }
        }
        return false;
    }

    virtual public void OnDie()
    {

    }
}
