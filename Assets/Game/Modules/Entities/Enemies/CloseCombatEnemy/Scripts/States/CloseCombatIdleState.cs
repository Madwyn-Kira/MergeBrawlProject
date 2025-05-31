public class CloseCombatIdleState : StateMachine
{
    private EnemyController _controller;

    public override void EnterState<T>(T controller)
    {
        _controller = controller as EnemyController;
        _controller.NavAgent.isStopped = true;

        _controller.OnStartWar += StartWar;
    }

    public override void ExitState()
    {
        _controller.OnStartWar -= StartWar;
    }

    public override void LocalUpdate()
    {

    }

    private void StartWar()
    {
        _controller.ChangeState(new CloseCombatFindAttackTargetState());
    }
}
