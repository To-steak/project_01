using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private float _timer;

    public EnemyChaseState(EnemyController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        Animations.PlayWalk(Config.WalkAnimSpeed);
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        Transform detected = Agent.DetectPlayer(_controller.transform.position);
        if (detected != null)
        {
            _controller.SetLastTarget(detected);
        }

        if (!_controller.HasLastTarget)
        {
            Agent.Stop();
            _controller.ChangeState(_controller.Move);
            return;
        }

        Transform target = _controller.LastTarget;
        var deltaTime = Time.deltaTime;

        Agent.RotateAgent(target.position, Config.RotationSpeed, deltaTime);

        if (Agent.TryReachedTarget(target.position, _controller.transform.position))
        {
            Agent.Stop();

            if (detected == null)
            {
                _controller.ClearLastTarget();
                _controller.ChangeState(_controller.Move);
                return;
            }

            _timer += deltaTime;
            if (_timer >= Config.AttackInterval)
            {
                _timer = 0f;
                _controller.ChangeState(_controller.Attack);
            }
            else
            {
                Animations.PlayIdle();
            }
        }
        else
        {
            Animations.PlayRun(Config.RunAnimSpeed);
            Agent.MoveTo(target.position, Config.RunSpeed);
        }
    }
}
