

public class StateMachine
{
    public State currentState;

    public void Initialize(State staringState)
    {
        currentState = staringState;
        staringState.Enter();
    }

    public void ChangeState(State newState)
    {
        currentState.Exit();

        currentState = newState;
        newState.Enter();
    }
}
