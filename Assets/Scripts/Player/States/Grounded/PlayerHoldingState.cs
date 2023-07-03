using Unity.VisualScripting;
using UnityEngine;
public class PlayerHoldingState : PlayerStandState
{
    private PhysicsProp prop;
    private Rigidbody propRB;
    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private Vector3 targetPropPosition;
    public PlayerHoldingState(PlayerController _controller, PhysicsProp _prop) : base(_controller)
    {
        prop = _prop;
        propRB = prop.GetComponent<Rigidbody>();
        initialRotation = Quaternion.Inverse(controller.PlayerCamera.transform.rotation) * prop.transform.rotation;
        targetRotation = initialRotation;
    }

    public override void EnterState()
    {
        base.EnterState();
        prop.onPickup();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        targetPropPosition = ConstrainProp();

        if (!prop.BeingHeld || ShouldDropProp()) {
            stateController.SwitchState(new PlayerStandState(controller));
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
   

        Vector3 targetPropVelocity = Vector3.ClampMagnitude(((targetPropPosition - prop.transform.position) * 1 / Time.fixedDeltaTime) + controller.Velocity, 15);


        float contactAmount = prop.IsContactingHeavyObject ? 0.1f : 1.0f;

        Vector3 adjustedVelocity = targetPropVelocity * contactAmount;

        propRB.velocity = adjustedVelocity;

        propRB.MoveRotation(targetRotation);
    }

    public override void HandleButtonMaps()
    {
        base.HandleButtonMaps();
        if (controller.PlayerInput.GetFirePressed()) {
            Vector3 throwVector = controller.PlayerCamera.transform.forward * Mathf.Max(3, controller.Velocity.magnitude);
            prop.Throw(throwVector);
        }
        if(controller.PlayerInput.GetUsePressed()) {
            prop.Drop();
        }
    }

    public override void ExitState()
    {
        prop.Drop();
        base.ExitState();
    }

    private Vector3 ConstrainProp()
    {
        Quaternion playerAngle = controller.PlayerCamera.transform.rotation;

        float pitch = Mathf.DeltaAngle(playerAngle.eulerAngles.x, 0);
        float clampX = Mathf.Clamp(pitch, -75, 45);

        Vector3 dir = Quaternion.Euler(-clampX, 0, 0) * Vector3.forward;

        dir = controller.transform.rotation * dir;
        targetRotation = Quaternion.LookRotation(dir) * initialRotation;
        return controller.PlayerCamera.transform.position + dir * 2.5f;
    }

    private bool ShouldDropProp()
    {
        return Vector3.Distance(prop.transform.position, targetPropPosition) >= 4;
    }
}