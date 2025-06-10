using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> ObjectsForPreparationStage = new();
    [SerializeField]
    public List<GameObject> ObjectsForFightStage = new();
    [SerializeField]
    public List<GameObject> ObjectsForEndFightStage = new();

    [SerializeField]
    public List<GameObject> ResultPanelObjects;

    public StateMachine CurrentState;
    public IFightCycle FightCycleController;

    private void Awake()
    {
        FightCycleController = GetComponent<IFightCycle>();
    }

    private void Update()
    {
        if (CurrentState != null)
            CurrentState.LocalUpdate();
    }

    public void ChangeActivateStateObjectsForStage(List<GameObject> objects, bool state)
    {
        foreach (var item in objects)
            item.gameObject.SetActive(state);
    }

    public void ChangeState(StateMachine newState)
    {
        if (CurrentState != null)
            CurrentState.ExitState();

        CurrentState = newState;
        CurrentState.EnterState(this);
    }
}
