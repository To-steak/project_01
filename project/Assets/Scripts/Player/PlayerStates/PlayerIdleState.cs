using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerController playerController) : base(playerController)
    {

    }

    public override void Enter()
    {
        Vector3 input = Inputs.Move;
        bool isRun = Inputs.Run;

        Movements.SetDirection(Vector3.zero);
        // Animations.PlayMove(input, isRun);
        Animations.PlayIdle();
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        Vector3 input = Inputs.Move;

        if (input != Vector3.zero)
        {
            _controller.ChangeState<PlayerMoveState>();
            return;
        }
    }
}