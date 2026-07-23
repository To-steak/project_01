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
        if (detected == null)
        {
            Agent.Stop();
            _controller.ChangeState(_controller.Move);
            return;
        }

        var deltaTime = Time.deltaTime;
        _timer += deltaTime;

        // Vector3 direction = detected.position - _controller.transform.position;
        // direction.y = 0;
        // if (direction.sqrMagnitude > DIRECTION_SQR_THRESHOLD)
        // {
        //     Quaternion rotation = Quaternion.LookRotation(direction);
        //     _controller.transform.rotation = Quaternion.Slerp(_controller.transform.rotation, rotation, Config.RotationSpeed * deltaTime);
        // }
        Agent.RotateAgent(detected.position, Config.RotationSpeed, deltaTime);

        if (Agent.TryReachedTarget(detected.position, _controller.transform.position))
        {
            Agent.Stop();

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
            Agent.MoveTo(detected.position, Config.RunSpeed);
        }
    }
}
