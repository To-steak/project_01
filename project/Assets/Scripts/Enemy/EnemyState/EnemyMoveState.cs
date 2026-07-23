using UnityEngine;

public class EnemyMoveState : EnemyState
{
    private float _timer;

    public EnemyMoveState(EnemyController controller) : base(controller)
    {
        _timer = 0;
    }

    public override void Enter()
    {
        _timer = 0;
        Agent.Stop();
        Animations.PlayIdle();
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        Transform detected = Agent.DetectPlayer(_controller.transform.position);
        if (detected != null)
        {
            _controller.ChangeState(_controller.Chase);
            return;
        }

        var deltaTime = Time.deltaTime;
        if (_timer < Config.NextMoveInterval)
        {
            _timer += deltaTime;
        }

        if (_timer >= Config.NextMoveInterval)
        {
            if (Agent.TryGetRandomDestination(_controller.transform.position, out Vector3 destination))
            {
                Agent.MoveTo(destination, Config.WalkSpeed);
            }

            _timer = 0f;
        }

        if (Agent.IsMoving)
        {
            Agent.RotateAgent(Agent.Destination, Config.RotationSpeed, deltaTime);
            Animations.PlayWalk(Config.WalkAnimSpeed);
        }
        else
        {
            Animations.PlayIdle();
        }
    }
}