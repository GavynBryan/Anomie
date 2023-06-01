using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput
{
    private readonly PlayerControls controls;
    private InputAction move;
    private InputAction look;
    private InputAction jump;
    public PlayerInput()
    {
        controls = new PlayerControls();

        move = controls.Grounded.Move;
        look = controls.Grounded.Look;
        jump = controls.Grounded.Jump;
    }

    public void EnableControls()
    {
        move.Enable();
        look.Enable();
        jump.Enable();
    }

    public void DisableControls()
    {
        move.Disable();
        look.Disable();
        jump.Disable();
    }

    public Vector3 GetMovementVector()
    {
        var raw = move.ReadValue<Vector2>();
        return new Vector3(raw.x, 0, raw.y);
    }

    public Vector2 GetLookVector()
    {
        return look.ReadValue<Vector2>();
    }
    public bool GetJumpPressed()
    {
        return jump.WasPressedThisFrame();
    }

    public bool GetJumpHeld()
    {
        return jump.IsPressed();
    }

}
