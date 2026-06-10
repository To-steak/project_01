using UnityEngine;

public class PlayerThrowState : PlayerState
{
    public PlayerThrowState(PlayerController playerController) : base(playerController)
    {
    }

    public override void Enter()
    {
        Movements.SetDirection(Vector3.zero);
        Animations.PlayThrow();
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {

    }

    public override void HandleAnimationFinish()
    {
        _controller.ChangeState(_controller.Idle);
    }

    public override void HandleAnimationCommit()
    {
        Weapons.TryAttack();
    }
}