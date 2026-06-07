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
        if (Inputs.Move == Vector2.zero)
        {
            _controller.ChangeState<PlayerIdleState>();
            return;
        }

        Movements.SetDirection(new Vector3(Inputs.Move.x, 0f, Inputs.Move.y));

        bool isRun = Inputs.Run;
        Movements.SetRunning(isRun);
        Animations.PlayMove(isRun);
    }
}