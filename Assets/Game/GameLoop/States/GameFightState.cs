public class GameFightState : StateMachine
{
    private Bootstrap _bootstrapController;

    public override void EnterState<T>(T controller)
    {
        _bootstrapController = controller as Bootstrap;

        _bootstrapController.ChangeActivateStateObjectsForStage(_bootstrapController.ObjectsForFightStage, true);
        _bootstrapController.FightCycleController.OnEndFight += EndFight;
    }

    public override void ExitState()
    {
        _bootstrapController.ChangeActivateStateObjectsForStage(_bootstrapController.ObjectsForFightStage, false);
        _bootstrapController.FightCycleController.OnEndFight -= EndFight;
    }

    public override void LocalUpdate()
    {

    }

    private void EndFight(bool isPlayerWin)
    {
        _bootstrapController.ChangeState(new GiveRewardsGameState());
    }
}
