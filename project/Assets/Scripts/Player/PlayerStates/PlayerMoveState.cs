using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(PlayerController playerController) : base(playerController)
    {
    }

    public override void Enter()
    {
        Animations.PlayWalk(true);
        Movements.SetRunning(false);
    }

    public override void Exit()
    {
        Animations.PlayWalk(false);
        Movements.SetRunning(false);
    }

    public override void Tick()
    {
        if (Inputs.Move == Vector2.zero)
        {
            _controller.ChangeState<PlayerIdleState>();
            return;
        }

        Movements.SetDirection(new Vector3(Inputs.Move.x, 0f, Inputs.Move.y));


        Animations.PlayRun(Inputs.Run);
        Movements.SetRunning(Inputs.Run);
    }
}