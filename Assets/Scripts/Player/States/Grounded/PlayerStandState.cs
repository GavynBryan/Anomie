using UnityEngine;
public class PlayerStandState : PlayerGroundedState
{
    public PlayerStandState(PlayerController _controller) : base(_controller)
    {
    }
    public override void HandleButtonMaps()
    {
        if(controller.PlayerInput.GetJumpPressed()) {
            stateController.SwitchState(new PlayerJumpState(controller));
        }
    }
}