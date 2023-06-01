
using UnityEngine; 

public class StateController
{
    protected State _currentState;
    public State CurrentState { get { return _currentState; } }
    public void SwitchState(State _state)
    {
        _state.StateController = this;

        _currentState?.ExitState();
        _currentState = _state;
        _currentState?.EnterState();
    }

    public void Update()
    {
        _currentState?.UpdateState();
    }

    public void LateUpdate()
    {
        _currentState?.LateUpdateState();
    }

    public void FixedUpdate()
    {
        _currentState?.FixedUpdateState();
    }
}
