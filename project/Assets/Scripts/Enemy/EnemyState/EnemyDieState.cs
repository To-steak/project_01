using UnityEngine;

public class EnemyDieState : EnemyState
{
    public EnemyDieState(EnemyController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        Agent.Stop();
        Animations.PlayDie();
        Collider.enabled = false;
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
    }

    public override void HandleAnimationFinish()
    {
        // 비활성화 후 Pool 반환
    }
}