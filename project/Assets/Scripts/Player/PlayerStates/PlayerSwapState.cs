public class PlayerSwapState : PlayerState
{
    public PlayerSwapState(PlayerController playerController) : base(playerController)
    {
    }

    public override void Enter()
    {
        Animations.PlaySwap();
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