using UnityEngine;
public class PlayerState : State
{
    protected readonly PlayerController controller;
    protected Vector3 movementInputVector;

    public PlayerState(PlayerController _controller)
    {
        controller = _controller;
    }
    protected virtual float acceleration
    {
        get { return controller.GroundFriction * controller.MoveSpeed; }
    }

    protected virtual float moveSpeed
    {
        get { return controller.MoveSpeed; }
    }
    public override void UpdateState()
    {
        movementInputVector = GetRelativeMovementVector();
    }
    protected Vector3 GetRelativeMovementVector()
    {
        return Quaternion.LookRotation(controller.transform.forward) * controller.PlayerInput.GetMovementVector();
    }

    public virtual void HandleButtonMaps() { }

    protected virtual void Accelerate()
    {
        Vector3 targetVelocity = controller.TargetVelocity; targetVelocity.y += controller.Velocity.y;
        controller.Velocity = Vector3.MoveTowards(controller.Velocity, targetVelocity, acceleration * Time.deltaTime);
    }

    protected virtual void ResolveInputVector(Vector3 _direction)
    {
        controller.TargetVelocity = _direction * moveSpeed;
    }

}