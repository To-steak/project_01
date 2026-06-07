using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(PlayerController playerController) : base(playerController)
    {
    }

    public override void Enter()
    {
        Movements.SetDirection(Vector3.zero);
        Animations.PlayShot(true);
    }

    public override void Exit()
    {
        Animations.PlayShot(false);
    }

    public override void Tick()
    {
        bool attack = Inputs.Attack;
        if (!attack)
        {
            _controller.ChangeState<PlayerIdleState>();
            return;
        }
    }
}