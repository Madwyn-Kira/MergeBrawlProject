public class CloseCombatFindAttackTargetState : StateMachine
{
    private EnemyController _controller;

    public override void EnterState<T>(T controller)
    {
        _controller = controller as EnemyController;
        _controller.NavAgent.isStopped = false;
    }

    public override void ExitState()
    {

    }

    public override void LocalUpdate()
    {

    }
}
