using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region References
    private Camera      mCamera;
    private Transform   player;

    private PlayerInput playerInput;
    #endregion

    #region Properties
    /// <summary>
    /// The mouse sensitivity along the x axis
    /// </summary>
    [SerializeField] private float xSensitivity = 1f;

    /// <summary>
    /// The mouse sensitivity along the Y axis
    /// </summary>
    [SerializeField] private float ySensitivity = 1f;

    /// <summary>
    /// Maximum camera Y angle
    /// </summary>
    [SerializeField] private float yCeiling = 88f;

    /// <summary>
    /// Minimum camera Y angle
    /// </summary>
    [SerializeField] private float yFloor = -88f;
    #endregion

    #region State
    /// <summary>
    /// Tells us whether or not we can move the mouse
    /// </summary>
    public bool isMousedLocked { get; private set; }

    /// <summary>
    /// The field of view for the camera
    /// </summary>
    public float FOV { get { return mCamera.fieldOfView; } private set { mCamera.fieldOfView = value; } }
    /// <summary>
    /// Current Y angle of the camera
    /// </summary>
    public float yAngle { get; private set; }

    /// <summary>
    /// Current X angle of the player
    /// </summary>
    public float xAngle { get; private set; }
    #endregion

    void Start()
    {
        player = transform.parent;
        playerInput = player.GetComponent<PlayerController>().PlayerInput;

        mCamera = GetComponent<Camera>();
    }

    void LateUpdate()
    {

        if (!isMousedLocked) {
            Vector2 LookAxis = playerInput.GetLookVector();
            xAngle += LookAxis.x * xSensitivity;
            xAngle = ClampAngle(xAngle, -360, 360);

            yAngle += -LookAxis.y * ySensitivity;
            yAngle = Mathf.Clamp(yAngle, yFloor, yCeiling);
        }
        transform.localRotation = Quaternion.Euler(yAngle, 0, 0);
        player.rotation = Quaternion.Euler(0, xAngle, 0);
    }

    public void AssignPlayerInput(PlayerInput _playerInput)
    {
        playerInput = _playerInput;
    }

    public void SetFOV(float _fov)
    {
        FOV = _fov;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
