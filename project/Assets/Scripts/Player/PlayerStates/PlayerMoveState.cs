using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private const float INPUT_SQRT_THRESHOLD = 0.01f;
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
        
        // Animations.PlayMove(input, isRun);
        Animations.PlayMove(isRun);
    }

    public override void HandleDodge()
    {
        if (Inputs.Move.sqrMagnitude < INPUT_SQRT_THRESHOLD)
        {
            return;
        }

        if (Movements.IsGround)
        {
            _controller.ChangeState<PlayerDodgeState>();
        }
    }
}