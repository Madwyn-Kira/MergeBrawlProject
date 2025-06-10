using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class Entity : MonoBehaviour, IEntityEvents
{
    public abstract WeaponController WeaponController { get; }

    protected HealthController _healthController;

    public StateMachine CurrentState;
    public BoardSpawnCell CurrentCell { get; private set; }
    public HealthController HealthController { get { return _healthController; } }

    [HideInInspector]
    public NavMeshAgent NavAgent;
    [HideInInspector]
    public ConfigSettings ConfigSettings;

    public event Action OnMerge;
    public event Action<Entity> OnSpawned;
    public event Action<Entity> OnDestroyEntity;
    public event Action OnStartWar;

    virtual public void InitializeParams()
    {
        NavAgent = GetComponent<NavMeshAgent>();
        _healthController = GetComponent<HealthController>();
    }

    virtual public void Initialize<T>(T newConfig)
    {
        ConfigSettings = newConfig as ConfigSettings;
    }

    abstract public void Fight();

    private void Update()
    {
        if (CurrentState != null)
            CurrentState.LocalUpdate();
    }

    public void AssignCell(BoardSpawnCell cell)
    {
        CurrentCell = cell;
    }

    virtual public void ChangeState(StateMachine newState)
    {
        if (CurrentState != null)
            CurrentState.ExitState();

        CurrentState = newState;
        CurrentState.EnterState(this);
    }

    virtual public void DestroyEntity()
    {
        OnDestroyEntity?.Invoke(this);
        //CurrentCell.transform.parent.GetComponent<IBoard>().UnregisterUnit(this, true);
        Destroy(gameObject);
    }
}
