using UnityEngine;

public class PlayerDodgeState : PlayerState
{
    public PlayerDodgeState(PlayerController playerController) : base(playerController)
    {
    }

    public override void Enter()
    {
        Movements.SetRotationLock(true);
        Movements.DoDodge();
        Animations.PlayDodge();
    }

    public override void Exit()
    {
        Movements.SetRotationLock(false);
    }

    public override void Tick()
    {

    }

    public override void HandleAnimationFinish()
    {
        _controller.ChangeState(_controller.Idle);
    }
}