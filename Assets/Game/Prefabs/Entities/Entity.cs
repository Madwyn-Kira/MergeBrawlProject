using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class Entity : MonoBehaviour
{
    public StateMachine CurrentState;

    [HideInInspector]
    public NavMeshAgent NavAgent;
    [HideInInspector]
    public Transform HeroTransform;
    [HideInInspector]
    public HeroConfig Config;
    [HideInInspector]
    public HeroEvolutionChainConfig EvolutionConfig;

    public event Action OnMerge;

    private void Start()
    {
        NavAgent = GetComponent<NavMeshAgent>();
        HeroTransform = GetComponent<Transform>();

        ChangeState(new HeroMergePreparationState());
    }

    abstract public void Fight();

    virtual public void ChangeState(StateMachine newState)
    {
        if (CurrentState != null)
            CurrentState.ExitState();

        CurrentState = newState;
        CurrentState.EnterState(this);
    }

    virtual public void Merge(Entity target)
    {

    }

    virtual public void OnDie()
    {

    }
}
