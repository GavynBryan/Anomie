
using Unity.VisualScripting;

public abstract class State
{
    protected StateController stateController;
    public StateController StateController { get { return stateController; } set { stateController = value; } }
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void UpdateState() { }
    public virtual void LateUpdateState() {  }
    public virtual void FixedUpdateState() {  }
}
