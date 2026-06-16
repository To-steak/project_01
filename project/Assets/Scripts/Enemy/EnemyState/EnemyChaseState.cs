using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private LayerMask _playerLayer;
    private float _attackInterval;
    private float _runSpeed;
    private float _runAnimSpeed;
    private float _radius;
    private float _rotationSpeed;
    private float _timer;
    private const float DIRECTION_SQR_THRESHOLD = 0.001f;

    public EnemyChaseState(EnemyController controller, EnemyConfig config) : base(controller)
    {
        _playerLayer = config.PlayerLayer;
        _attackInterval = config.AttackInterval;
        _runSpeed = config.RunSpeed;
        _runAnimSpeed = config.RunAnimSpeed;
        _radius = config.MaxRadius;
        _rotationSpeed = config.RotationSpeed;
    }

    public override void Enter()
    {
        Animations.PlayWalk(1);
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        Transform detected = Agent.DetectPlayer(_controller.transform.position, _radius, _playerLayer);
        if (detected == null)
        {
            Agent.Stop();
            _controller.ChangeState(_controller.Move);
            return;
        }

        var deltaTime = Time.deltaTime;
        _timer += deltaTime;

        if (Agent.TryReachedTarget(detected.position, _controller.transform.position))
        {
            if (_timer >= _attackInterval)
            {
                _timer = 0f;
                Agent.Stop();
                _controller.ChangeState(_controller.Attack);
            }
            else
            {
                Animations.PlayIdle();

                Vector3 direction = detected.position - _controller.transform.position;
                direction.y = 0;
                if (direction.sqrMagnitude > DIRECTION_SQR_THRESHOLD)
                {
                    Quaternion rotation = Quaternion.LookRotation(direction);
                    _controller.transform.rotation = Quaternion.Slerp(_controller.transform.rotation, rotation, _rotationSpeed * deltaTime);
                }
            }
        }
        else
        {
            Animations.PlayRun(_runAnimSpeed);
            Agent.MoveTo(detected.position, _runSpeed);
        }
    }
}
