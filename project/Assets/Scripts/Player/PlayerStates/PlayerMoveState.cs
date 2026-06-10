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
        if (input == Vector3.zero)
        {
            _controller.ChangeState(_controller.Idle);
            return;
        }

        Movements.SetDirection(input);

        bool isRun = Inputs.Run;
        Movements.SetRunning(isRun);
        Animations.PlayMove(isRun);

        bool attack = Inputs.Attack;
        if (attack)
        {
            var state = Weapons.GetAttackState(_controller);
            _controller.ChangeState(state);
            return;
        }
    }

    public override void HandleDodge()
    {
        if (Inputs.Move.sqrMagnitude < INPUT_SQRT_THRESHOLD)
        {
            return;
        }

        if (Movements.IsGround)
        {
            _controller.ChangeState(_controller.Dodge);
        }
    }

    public override void HandleSwap(int index)
    {
        if (Movements.IsGround)
        {
            if (Weapons.TrySwapWeapon(index))
            {
                _controller.ChangeState(_controller.Swap);
            }
        }
    }

    public override void HandleReload()
    {
        if (Movements.IsGround)
        {
            if (Weapons.TryReload())
            {
                _controller.ChangeState(_controller.Reload);
            }
        }
    }
}