using UnityEngine;

public abstract class PlayerState
{
    protected PlayerController _controller;
    protected PlayerInputs Inputs => _controller.Inputs;
    protected PlayerMovements Movements => _controller.Movements;
    protected PlayerAnimations Animations => _controller.Animations;
    protected PlayerWeapons Weapons => _controller.Weapons;

    public PlayerState(PlayerController playerController)
    {
        _controller = playerController;
    }

    public abstract void Enter();
    public abstract void Tick();
    public abstract void Exit();
    public virtual void HandleDodge() { }
    public virtual void HandleSwap(int index) { }
    public virtual void HandleAnimationFinish() { }
    public virtual void HandleAnimationCommit() { }
}
