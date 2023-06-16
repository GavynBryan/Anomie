using Unity.VisualScripting;
using UnityEngine;
public class PlayerAirborneState : PlayerState
{
    protected override float acceleration { get { return controller.AirFriction * controller.MoveSpeed; } }
    public PlayerAirborneState(PlayerController _controller) : base(_controller)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        ResolveInputVector(movementInputVector);
        Accelerate();

        controller.ApplyGravity();

        if (controller.Velocity.y <= 0) {
            if (controller.IsGrounded) {
                controller.OnLand();
                stateController.SwitchState(new PlayerStandState(controller));
            }
        }

        HandleButtonMaps();
    }
}