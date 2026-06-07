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
        Vector3 input = Inputs.Move;
        if (input != Vector3.zero)
        {
            _controller.ChangeState<PlayerMoveState>();
            return;
        }

        bool attack = Inputs.Attack;
        if (attack)
        {
            _controller.ChangeState<PlayerAttackState>();
            return;
        }
    }
}