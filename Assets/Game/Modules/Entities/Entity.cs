using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class Entity : MonoBehaviour, IEntityEvents
{
    public abstract WeaponController WeaponController { get; }

    protected HealthController _healthController;

    public StateMachine CurrentState;

    [HideInInspector]
    public NavMeshAgent NavAgent;

    public event Action OnMerge;
    public event Action<Entity> OnSpawned;
    public event Action<Entity> OnDestroy;
    public event Action OnStartWar;

    virtual public void InitializeParams()
    {
        NavAgent = GetComponent<NavMeshAgent>();
        _healthController = GetComponent<HealthController>();
    }

    virtual public void Initialize<T>(T newConfig)
    {

    }

    abstract public void Fight();

    virtual public void ChangeState(StateMachine newState)
    {
        if (CurrentState != null)
            CurrentState.ExitState();

        CurrentState = newState;
        CurrentState.EnterState(this);
    }

    virtual public void OnDestroyEntity()
    {
        OnDestroy?.Invoke(this);
        Destroy(gameObject);
    }
}
