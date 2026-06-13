using UnityEngine;

public class PlayerSwapState : PlayerState
{
    public PlayerSwapState(PlayerController playerController) : base(playerController)
    {
    }

    public override void Enter()
    {
        Movements.SetDirection(Vector3.zero);
        Animations.PlaySwap();
        Animations.SetAttackSpeed(Weapons.AttackSpeed);
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
}