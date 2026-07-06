public class EnemyDieState : EnemyState
{
    public EnemyDieState(EnemyController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        Agent.Stop();
        Animations.PlayDie();
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
    }
}