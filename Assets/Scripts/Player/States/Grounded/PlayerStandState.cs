using UnityEngine;
public class PlayerStandState : PlayerGroundedState
{
    public PlayerStandState(PlayerController _controller) : base(_controller)
    {
    }
    public override void HandleButtonMaps()
    {
        base.HandleButtonMaps();
        if(controller.PlayerInput.GetJumpPressed()) {
            stateController.SwitchState(new PlayerJumpState(controller));
        }
    }
}