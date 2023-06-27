using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput
{
    private readonly PlayerControls controls;
    private InputAction move;
    private InputAction look;
    private InputAction jump;
    private InputAction fire;

    private Vector2 smoothLookVector = Vector2.zero;
    public PlayerInput()
    {
        controls = new PlayerControls();

        move = controls.Grounded.Move;
        look = controls.Grounded.Look;
        jump = controls.Grounded.Jump;
        fire = controls.Grounded.Fire;
    }

    public void EnableControls()
    {
        move.Enable();
        look.Enable();
        jump.Enable();
        fire.Enable();
    }

    public void DisableControls()
    {
        move.Disable();
        look.Disable();
        jump.Disable();
        fire.Disable();
    }

    public Vector3 GetMovementVector()
    {
        var raw = move.ReadValue<Vector2>();
        return new Vector3(raw.x, 0, raw.y);
    }

    public Vector2 GetLookVector()
    {
        var raw = look.ReadValue<Vector2>();
        smoothLookVector = Vector2.Lerp(smoothLookVector, raw, 35 * Time.deltaTime);
        return smoothLookVector;
    }
    public bool GetJumpPressed()
    {
        return jump.WasPressedThisFrame();
    }

    public bool GetJumpHeld()
    {
        return jump.IsPressed();
    }

    public bool GetFirePressed()
    {
        return fire.WasPressedThisFrame();
    }

    public bool GetFireHeld()
    {
        return fire.IsPressed();
    }
}
