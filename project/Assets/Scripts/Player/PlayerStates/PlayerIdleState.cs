using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerController playerController) : base(playerController)
    {

    }

    public override void Enter()
    {
        Movements.SetDirection(Vector3.zero);
        Animations.PlayIdle();
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        if (Inputs.Move != Vector2.zero)
        {
            _controller.ChangeState<PlayerMoveState>();
        }
    }
}