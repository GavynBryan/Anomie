using UnityEngine;

public partial class PlayerController : MonoBehaviour, IDamageable
{
    [Header("Camera Properties")]
    [SerializeField] private float cameraSensitivityX;
    [SerializeField] private float cameraSensitivityY;
    [SerializeField] private float yFloor;
    [SerializeField] private float yCeiling;
    [SerializeField] private float cameraFOV;

    private float cameraX;
    private float cameraY;

    public float CameraSensitivityX { get { return cameraSensitivityX; } }
    public float CameraSensitivityY { get { return cameraSensitivityY; } }
    public float CameraFOV { get { return cameraFOV; } }

    public void MoveCamera()
    {
        Vector2 LookAxis = playerInput.GetLookVector();
        cameraX = (cameraX + (LookAxis.x * cameraSensitivityX)).WrapToAngle();
        cameraY += -LookAxis.y * CameraSensitivityY;
        cameraY = Mathf.Clamp(cameraY, -88, 88);

        //adjust camera
        playerCamera.transform.rotation = Quaternion.Euler(cameraY, cameraX, 0);
        playerCamera.transform.position = transform.position + (transform.up * 1.75f);
    }
}
