using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private LayerMask _playerLayer;
    private LayerMask _obstacleLayer;
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
        _obstacleLayer = config.ObstacleLayer;
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
        Transform detected = Agent.DetectPlayer(_controller.transform.position, _radius, _playerLayer, _obstacleLayer);
        // 감지 사거리를 벗어났을 때
        if (detected == null)
        {
            Agent.Stop();
            _controller.ChangeState(_controller.Move);
            return;
        }

        var deltaTime = Time.deltaTime;
        _timer += deltaTime;

        // 공격 사거리 내에 Player가 존재할 때
        if (Agent.TryReachedTarget(detected.position, _controller.transform.position))
        {
            // 일단 멈춘다.
            Agent.Stop();

            if (_timer >= _attackInterval) // 공격 쿨타임이 찼다면
            {
                _timer = 0f;
                _controller.ChangeState(_controller.Attack);
            }
            else // 공격 쿨타임이 안 찼다면
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
        else // 공격 사거리를 벗어났을 때
        {
            Animations.PlayRun(_runAnimSpeed);
            Agent.MoveTo(detected.position, _runSpeed);
        }
    }
}
