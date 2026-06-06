using UnityEngine;

public abstract class PlayerState
{
    protected PlayerController _controller;
    protected PlayerInputs Inputs => _controller.Inputs;
    protected PlayerMovements Movements => _controller.Movements;
    protected PlayerAnimations Animations => _controller.Animations;

    public PlayerState(PlayerController playerController)
    {
        _controller = playerController;
    }

    public abstract void Enter();
    public abstract void Tick();
    public abstract void Exit();
    public virtual void HandleJump() { }
}
