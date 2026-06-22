using UnityEngine;

public abstract class EnemyState
{
    protected EnemyController _controller;
    protected EnemyAgent Agent => _controller.Agent;
    protected EnemyHealth Health => _controller.Health;
    protected EnemyAnimations Animations => _controller.Animations;
    protected EnemyWeapon Weapon => _controller.Weapon;
    protected EnemyConfig Config => _controller.Config;

    public EnemyState(EnemyController controller)
    {
        _controller = controller;
    }

    public abstract void Enter();
    public abstract void Tick();
    public abstract void Exit();
    // Animation
    public virtual void HandleAnimationFinish() { }
    public virtual void HandleAnimationCommit() { }
}
