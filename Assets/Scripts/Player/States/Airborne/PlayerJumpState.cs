using UnityEngine;

public class PlayerJumpState : PlayerAirborneState
{
    private Vector3 jumpDir;
    public PlayerJumpState(PlayerController _controller) : base(_controller)
    {
    }

    public override void EnterState()
    {
        var newVelocity = controller.Velocity;
        newVelocity.y = controller.JumpForce;
        //Don't enter this state if we aren't going to jump high enough
        if (newVelocity.y < 0.5) return;

        //If the player is moving in the direction they're jumping in, accelerate them
        Vector3 input = GetRelativeMovementVector();
        //No need to normalize the vector, we are only comparing angles
        jumpDir = controller.Velocity.Flatten();
        if (Vector3.Angle(input, jumpDir) <= 25  && jumpDir.magnitude > 1) {
            newVelocity.x *= 1.25f;
            newVelocity.z *= 1.25f;
        }else {
            newVelocity.x = input.x * moveSpeed;
            newVelocity.z = input.z * moveSpeed;
        }
        controller.Velocity = newVelocity;
        base.EnterState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(controller.Velocity.y < 0) {
            stateController.SwitchState(new PlayerAirborneState(controller));
        }
    }

    public override void HandleButtonMaps()
    {
        Vector3 velocity = controller.Velocity;
        if (!controller.PlayerInput.GetJumpHeld() && velocity.y > 1) {
            velocity.y /= 2;
            controller.Velocity = velocity;
            stateController.SwitchState(new PlayerAirborneState(controller));
        }
    }
}
