using UnityEngine;

public class PlayerShotState : PlayerState
{
    public PlayerShotState(PlayerController playerController) : base(playerController)
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
            _controller.ChangeState(_controller.Idle);
            return;
        }
    }

    public override void HandleAnimationFinish()
    {
        Debug.Log("Finished");
    }

    public override void HandleAnimationCommit()
    {
        Debug.Log("Shot");
    }
}