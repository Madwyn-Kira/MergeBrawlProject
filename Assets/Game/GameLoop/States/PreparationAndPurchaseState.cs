public class PreparationAndPurchaseState : StateMachine
{
    private Bootstrap _bootstrapController;

    public override void EnterState<T>(T controller)
    {
        _bootstrapController = controller as Bootstrap;

        _bootstrapController.ChangeActivateStateObjectsForStage(_bootstrapController.ObjectsForPreparationStage, true);
        _bootstrapController.FightCycleController.OnStartFight += StartFight;
    }

    public override void ExitState()
    {
        _bootstrapController.ChangeActivateStateObjectsForStage(_bootstrapController.ObjectsForPreparationStage, false);
        _bootstrapController.FightCycleController.OnStartFight -= StartFight;
    }

    public override void LocalUpdate()
    {

    }

    private void StartFight()
    {
        _bootstrapController.ChangeState(new GameFightState());
    }
}
