public class EnemyDieState : EnemyState
{
    public EnemyDieState(EnemyController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        Animations.PlayDie();
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
    }
}