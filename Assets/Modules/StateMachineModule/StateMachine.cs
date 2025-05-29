public abstract class StateMachine
{
    public abstract void EnterState<T>(T controller);

    public abstract void ExitState();

    public abstract void LocalUpdate();
}
