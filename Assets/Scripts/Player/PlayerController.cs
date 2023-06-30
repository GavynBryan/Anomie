using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerController : MonoBehaviour, IDamageable
{
    private CharacterController characterController;

    private PlayerInput playerInput;
    private StateController stateController;
    [SerializeField]
    private Camera playerCamera;

    public CharacterController CharacterController { get { return characterController; } }
    public PlayerInput PlayerInput { get { return playerInput; } }
    public Camera PlayerCamera { get { return playerCamera; } }
    
    PlayerController() 
    {
        health = new Stat(100, 100);
        stateController = new StateController();
    }
    
    void Awake()
    {
        playerInput = new PlayerInput();
        stateController.SwitchState(new PlayerStandState(this));  
    }

    void OnEnable()
    {
        playerInput.EnableControls();
    }

    void OnDisable()
    {
        playerInput.DisableControls();
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        
        groundedCast += characterController.skinWidth;
        airborneGroundedCast += characterController.skinWidth;

        //For testing purposes only, revise later
        currentWeapon = GetComponentInChildren<WeaponBase>();
        currentWeapon.AssignToPlayer(this);
        weaponHolder = playerCamera.transform.Find("Weapon");
        
        ApplyLegModule(legModule);
    }

    void Update()
    {
        MoveCamera();
        isGrounded = CheckGrounded();
        if (isGrounded) {
            lastGrounded = Time.time;
        }

        stateController.Update();
        characterController.Move(velocity * Time.deltaTime);
    }

    void FixedUpdate()
    {
        stateController.FixedUpdate();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 300, 20), stateController.CurrentState.ToString());
        GUI.Label(new Rect(0, 20, 300, 20), "Ground Angle: " + groundAngle.ToString());
        GUI.Label(new Rect(0, 40, 300, 20), "Is Grounded: " + isGrounded.ToString());
        GUI.Label(new Rect(0, 60, 300, 20), "Target Velocity: " + targetVelocity.ToString());
        GUI.Label(new Rect(0, 80, 300, 20), "Current Velocity: " + velocity.ToString());
        if(ShouldSlide)
            GUI.Label(new Rect(0, 100, 300, 20), "Is On Step: " + IsOnStep().ToString());

        GUI.Label(new Rect(0, Screen.height - 30, 300, 20), "Health: " + health.value);
    }
}
