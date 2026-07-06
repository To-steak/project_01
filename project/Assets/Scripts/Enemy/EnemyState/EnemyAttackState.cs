using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(EnemyController controller) : base(controller)
    {

    }

    public override void Enter()
    {
        Animations.PlayAttack();
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        
    }

    public override void HandleAnimationCommit()
    {
        Vector3 position = _controller.transform.position + (_controller.transform.forward * 1);
        if (Weapon.TryAttack(position))
        {
            
        }
    }

    public override void HandleAnimationFinish()
    {
        _controller.ChangeState(_controller.Move);
    }
}