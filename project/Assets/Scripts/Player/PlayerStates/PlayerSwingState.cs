using UnityEngine;

public class PlayerSwingState : PlayerState
{
    public PlayerSwingState(PlayerController playerController) : base(playerController)
    {
    }

    public override void Enter()
    {
        Movements.SetDirection(Vector3.zero);
        Animations.PlaySwing(true);
    }

    public override void Exit()
    {
        Animations.PlaySwing(false);
    }

    public override void Tick()
    {

    }

    public override void HandleAnimationFinish()
    {
        if (!Inputs.Attack)
        {
            _controller.ChangeState(_controller.Idle);
        }
    }

    public override void HandleAnimationCommit()
    {
        if (Weapons.TryAttack(Movements.MouseWorldPosition))
        {

        }
    }
}