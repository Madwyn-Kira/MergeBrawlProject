public class GiveRewardsGameState : StateMachine
{
    private Bootstrap _bootstrapController;

    public override void EnterState<T>(T controller)
    {
        _bootstrapController = controller as Bootstrap;

        _bootstrapController.ChangeActivateStateObjectsForStage(_bootstrapController.ResultPanelObjects, true);
        _bootstrapController.FightCycleController.OnEndFight += EndFight;
    }

    public override void ExitState()
    {
        _bootstrapController.ChangeActivateStateObjectsForStage(_bootstrapController.ResultPanelObjects, false);
        _bootstrapController.FightCycleController.OnEndFight -= EndFight;
    }

    public override void LocalUpdate()
    {

    }

    private void EndFight(bool isPlayerWin)
    {
        _bootstrapController.ChangeState(new PreparationAndPurchaseState());
    }
}
