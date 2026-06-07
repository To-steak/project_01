using UnityEngine;

public class PlayerDodgeState : PlayerState
{
    public PlayerDodgeState(PlayerController playerController) : base(playerController)
    {
    }

    public override void Enter()
    {
        Vector3 input = Inputs.Move;
        
        Movements.SetRotationLock(true);
        Movements.SetDirection(input);
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
        _controller.ChangeState<PlayerIdleState>();
    }
}