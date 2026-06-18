using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(EnemyController controller) : base(controller)
    {

    }

    public override void Enter()
    {
        Animations.PlayAttack();
        Weapon.TryAttack();
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        
    }

    public override void HandleAnimationCommit()
    {
        // 여기서 피해를 주는 코드
    }

    public override void HandleAnimationFinish()
    {
        _controller.ChangeState(_controller.Move);
    }
}