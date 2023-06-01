using UnityEngine;

public partial class PlayerController : MonoBehaviour
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
    [SerializeField] private float groundedCast;

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
    public Vector3 Velocity { get { return velocity; } set { velocity = value; } }
    public Vector3 TargetVelocity { get { return targetVelocity; } set { targetVelocity = value; } }
    public bool IsGrounded { get { return isGrounded; } }
    public RaycastHit CurrentGround { get { return currentGround; } }
    public float GroundAngle { get { return groundAngle; } }
    #endregion

    #region Methods
    private bool CheckGrounded()
    {
        int mask = 1;
        bool cast = Physics.SphereCast(transform.position, characterController.radius, -transform.up, out currentGround, groundedCast, mask, QueryTriggerInteraction.Ignore);
        groundAngle = Vector3.Angle(currentGround.normal, transform.up);
        return cast;

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



    private void ApplyGravity()
    {
        isGrounded = CheckGrounded();
        if (isGrounded) {
            lastGrounded = Time.time;
        } else { velocity.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime; }
    }
    #endregion
}
