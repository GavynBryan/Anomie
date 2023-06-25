using UnityEngine;

public partial class PlayerController : MonoBehaviour, IDamageable
{
    [Header("Camera Properties")]
    [SerializeField] private float cameraSensitivityX;
    [SerializeField] private float cameraSensitivityY;
    [SerializeField] private float yFloor;
    [SerializeField] private float yCeiling;
    [SerializeField] private float cameraFOV;
    
    [SerializeField] private float weaponSway = 5;
    [SerializeField] private float maxWeaponSwayAngle = 10;
    [SerializeField] private float weaponSmoothing = 6;

    private float cameraX;
    private float cameraY;

    public float CameraSensitivityX { get { return cameraSensitivityX; } }
    public float CameraSensitivityY { get { return cameraSensitivityY; } }
    public float CameraFOV { get { return cameraFOV; } }

    public void MoveCamera()
    {
        Vector2 lookAxis = playerInput.GetLookVector();
        cameraX = (cameraX + (lookAxis.x * cameraSensitivityX)).WrapToAngle();
        cameraY += -lookAxis.y * CameraSensitivityY;
        cameraY = Mathf.Clamp(cameraY, yFloor, yCeiling);

        //adjust camera
        transform.localRotation = Quaternion.Euler(0, cameraX, 0);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraY, 0, 0);
        SwayWeapon(lookAxis);
    }

    public void SwayWeapon(Vector2 cameraVelocity)
    {
        float xMultiplier = Mathf.Clamp(cameraVelocity.x * weaponSway, -maxWeaponSwayAngle, maxWeaponSwayAngle);
        float yMultiplier = cameraVelocity.y * weaponSway * Mathf.InverseLerp(yCeiling, 0, Mathf.Abs(cameraY));

        Quaternion sway = Quaternion.Euler(yMultiplier, -xMultiplier, 0);

        weaponHolder.transform.localRotation = Quaternion.Slerp(weaponHolder.transform.localRotation,
            sway, weaponSmoothing * Time.deltaTime);
    }
}
