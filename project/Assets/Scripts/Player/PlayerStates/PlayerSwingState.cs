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
        bool attack = Inputs.Attack;
        if (!attack)
        {
            _controller.ChangeState(_controller.Idle);
            return;
        }
    }
}