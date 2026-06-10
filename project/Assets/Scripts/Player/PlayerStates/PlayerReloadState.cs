using UnityEngine;

public class PlayerReloadState : PlayerState
{
    public PlayerReloadState(PlayerController playerController) : base(playerController)
    {
    }

    public override void Enter()
    {
        Movements.SetDirection(Vector3.zero);
        Animations.PlayReload();
    }

    public override void Exit()
    {
        
    }

    public override void Tick()
    {
        // throw new System.NotImplementedException();
        // Cancle 키 만들까 말까? 'x' 눌러서 장전 중 캔슬
    }

    public override void HandleAnimationFinish()
    {
        _controller.ChangeState(_controller.Idle);
    }
}
