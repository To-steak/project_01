using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(PlayerController playerController) : base(playerController)
    {
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        Vector3 input = Inputs.Move;
        bool isRun = Inputs.Run;

        if (input == Vector3.zero)
        {
            _controller.ChangeState<PlayerIdleState>();
            return;
        }

        Movements.SetDirection(input);
        Movements.SetRunning(isRun);
        Animations.PlayMove(isRun);
    }

    public override void HandleDodge()
    {
        if (Movements.IsGround)
        {
            _controller.ChangeState<PlayerDodgeState>();
        }
    }
}