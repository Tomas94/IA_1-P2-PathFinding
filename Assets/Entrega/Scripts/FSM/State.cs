public abstract class State
{
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
    public abstract void Transitions();

    public FiniteStateMachine fsm;
}
