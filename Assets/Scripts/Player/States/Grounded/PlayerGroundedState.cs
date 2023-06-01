using UnityEngine;
public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(PlayerController _controller) : base(_controller)
    {
    }

    protected Vector3 SlopeDirection
    {
        get
        {
            Vector3 groundNormal = controller.CurrentGround.normal;
            Vector3 cross = Vector3.Cross(groundNormal, Vector3.down);
            return Vector3.Cross(cross, groundNormal);
        }
    }

    protected override void Accelerate()
    {
        Vector3 targetVelocity = controller.TargetVelocity; targetVelocity.y += controller.Velocity.y;
        //Since we're aligning input along the slope, the interpolation needs to happen with slope alignment as a starting point
        Vector3 correctedVelocity = Vector3.ProjectOnPlane(controller.Velocity, controller.CurrentGround.normal);
        controller.Velocity = Vector3.MoveTowards(correctedVelocity, targetVelocity, acceleration * Time.deltaTime);
    }

    protected virtual float GetSlopeSpeed()
    {
        float t = Mathf.InverseLerp(20, 60, controller.GroundAngle);
        float deccelerator = Mathf.Lerp(0, 1, t);
        return Mathf.MoveTowards(controller.Velocity.Flatten().magnitude, 0, deccelerator * acceleration * Time.deltaTime);
    }

    protected override void ResolveInputVector(Vector3 _direction)
    {
        //Prevent slope bouncing
        Vector3 projectedVelocity = Vector3.ProjectOnPlane(_direction, controller.CurrentGround.normal);

        float targetSpeed = moveSpeed;
        if (controller.ShouldSlide) {
            //Let player fight the decceleration until their velocity gets to low or they turn in the direction of the slope
            if (controller.Velocity.Flatten().magnitude <= .01f || Vector3.Angle(controller.Velocity, -SlopeDirection) >= 85) {
                stateController.SwitchState(new PlayerSlideState(controller));
            } else {
                targetSpeed = GetSlopeSpeed();
            }
        }

        controller.TargetVelocity = (projectedVelocity * targetSpeed);


    }

    public override void EnterState()
    {
        base.EnterState();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (!controller.isSoftGrounded) {
            stateController.SwitchState(new PlayerAirborneState(controller));
            return;
        }

        ResolveInputVector(movementInputVector);
        Accelerate();

        HandleButtonMaps();
    }

}