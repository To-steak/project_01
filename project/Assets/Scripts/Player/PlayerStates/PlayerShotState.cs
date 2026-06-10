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
        // Weapons.Attack();
        Debug.Log("Attack");
    }
}