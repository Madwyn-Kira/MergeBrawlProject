public class HeroMergePreparationState : StateMachine
{
    private Entity _controller;

    public override void EnterState<T>(T controller)
    {
        _controller = controller as Entity;

        _controller.OnMerge += SucessMerge;
        _controller.NavAgent.enabled = false;
    }

    public override void ExitState()
    {
        _controller.OnMerge -= SucessMerge;
        _controller.NavAgent.enabled = true;
    }

    public override void LocalUpdate()
    {

    }

    public void SucessMerge()
    {

    }
}
