﻿using UnityEngine;
using UnityEngine.InputSystem.XR;

public partial class PlayerController : MonoBehaviour, IDamageable
{
    #region Properties
    [Header("Movement Properties")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private float jumpForce;
    [SerializeField] private float airFriction;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private float groundFriction;
    [SerializeField] private float slideAngle;
    [SerializeField] private float slideFriction;
    [SerializeField] private float groundedCast;
    [SerializeField] private float airborneGroundedCast;

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetVelocity = Vector3.zero;

    private bool isGrounded;
    private RaycastHit currentGround;

    private float groundAngle;
    private float lastGrounded = 0;

    public float MoveSpeed { get { return moveSpeed; } }
    public float JumpForce { get { return jumpForce; } }
    public float AirFriction { get { return airFriction; } }
    public float GroundFriction { get { return groundFriction; } }
    public float GravityMultiplier { get { return gravityMultiplier; } }
    public float SlideAngle { get { return slideAngle; } }
    public float SlideFriction { get { return slideFriction; } }
    public Vector3 Velocity { get { return velocity; } set { velocity = value; } }
    public Vector3 TargetVelocity { get { return targetVelocity; } set { targetVelocity = value; } }
    public Vector3 TargetDirection { get { return Quaternion.Euler(0, cameraX, 0) * Vector3.forward; } }
    public bool IsGrounded { get { return isGrounded; } }
    public RaycastHit CurrentGround { get { return currentGround; } }
    public float GroundAngle { get { return groundAngle; } }
    #endregion

    #region Methods
    private bool CheckGrounded()
    {
        float dist = isGrounded ? groundedCast : airborneGroundedCast;
        int mask = 1;
        bool cast = Physics.CapsuleCast(GetCapsuleBottomCenterpoint(), GetCapsuleTopCenterpoint(characterController.height),
                    characterController.radius, Vector3.down, out currentGround, dist, mask,
                    QueryTriggerInteraction.Ignore);
        groundAngle = Vector3.Angle(currentGround.normal, transform.up);
        return cast;

    }

    public bool IsOnStep()
    {
        RaycastHit ground;
        Vector3 origin = new Vector3(transform.position.x, currentGround.point.y, transform.position.z);
        Debug.DrawLine(origin, currentGround.point + (Vector3.down * 0.4f), Color.red);
        if (Physics.Raycast(origin, Vector3.down, out ground, 0.4f, 1, QueryTriggerInteraction.Ignore)) {
            Debug.DrawLine(ground.point, ground.point + (ground.normal * 1), Color.green);
            if (Vector3.Angle(ground.normal, Vector3.up) < 10) {
                return true;
            }
        }
        return false;
    }

    Vector3 GetCapsuleBottomCenterpoint()
    {
        return transform.position + (transform.up * characterController.radius);
    }
   
    Vector3 GetCapsuleTopCenterpoint(float _height)
    {
        return transform.position + (transform.up * (_height - characterController.radius));
    }

    /// <summary>
    /// Allowance timer so that player can jump a few frames after losing their footing
    /// </summary>
    public bool isSoftGrounded
    {
        get
        {
            if (Time.time - lastGrounded <= .1) {
                return true;
            }
            return false;
        }
    }

    public bool ShouldSlide
    {
        get { return groundAngle >= slideAngle; }
    }

    public void ApplyGravity()
    {
        velocity.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
    }

    protected void OrientPlayerToDirection()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(TargetDirection, transform.up),
            3.5f * Time.deltaTime);
    }
    #endregion
}
