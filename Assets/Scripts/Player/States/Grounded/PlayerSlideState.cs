using UnityEngine;
public class PlayerSlideState : PlayerGroundedState
{
    public PlayerSlideState(PlayerController _controller) : base(_controller)
    {
    }
    protected override float acceleration
    {
        get { return controller.SlideFriction * controller.MoveSpeed; }
    }
    protected override float GetSlopeSpeed()
    {
        float t = Mathf.InverseLerp(0, 60, controller.GroundAngle);
        float accelerator = Mathf.Lerp(1, 45, t);
        return t * accelerator;
    }

    protected override void ResolveInputVector(Vector3 _direction)
    {
        float slopeSpeed = GetSlopeSpeed();

        Vector3 slideVelocity = controller.ShouldSlide ? 
            Vector3.ProjectOnPlane(Physics.gravity * slopeSpeed + _direction, controller.CurrentGround.normal) : Vector3.zero;

        Vector3 correctedVelocity = Vector3.ProjectOnPlane(controller.Velocity, controller.CurrentGround.normal);
        Vector3 deltaVelocity = slideVelocity - correctedVelocity;

        Vector3 slideDelta = Vector3.ClampMagnitude(deltaVelocity, Mathf.Max(15, slopeSpeed) * Time.deltaTime);

        Vector3 correctionDelta = correctedVelocity - controller.Velocity;

        controller.Velocity += slideDelta + correctionDelta;
    }

    public override void FixedUpdateState()
    {
        base.UpdateState();
        if (!controller.ShouldSlide)
            if(controller.Velocity.magnitude <= 0.2f || controller.PlayerInput.GetMovementVector().magnitude > 0) {
                stateController.SwitchState(new PlayerStandState(controller));
        }
    }

}