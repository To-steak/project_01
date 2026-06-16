using UnityEngine;

public class EnemyMoveState : EnemyState
{
    private LayerMask _playerLayer;
    private float _walkSpeed;
    private float _walkAnimSpeed;
    private float _minRadius;
    private float _maxRadius;
    private float _interval;
    private float _timer;

    public EnemyMoveState(EnemyController controller, EnemyConfig config) : base(controller)
    {
        _playerLayer = config.PlayerLayer;
        _walkSpeed = config.WalkSpeed;
        _walkAnimSpeed = config.WalkAnimSpeed;
        _minRadius = config.MinRadius;
        _maxRadius = config.MaxRadius;
        _interval = config.NextMoveInterval;
        _timer = 0;
    }

    public override void Enter()
    {
        _timer = 0;
        Agent.MoveTo(_controller.transform.position, _walkSpeed);
        Animations.PlayIdle();
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        Transform detected = Agent.DetectPlayer(_controller.transform.position, _maxRadius, _playerLayer);
        if (detected != null)
        {
            _controller.ChangeState(_controller.Chase);
            return;
        }

        var deltaTime = Time.deltaTime;
        if (_timer < _interval)
        {
            _timer += deltaTime;
        }

        if (_timer >= _interval)
        {
            if (Agent.TryGetRandomDestination(_controller.transform.position, _minRadius, _maxRadius, out Vector3 destination))
            {
                Agent.MoveTo(destination, _walkSpeed);
            }

            _timer = 0f;
        }

        if (Agent.IsMoving)
        {
            Animations.PlayWalk(_walkAnimSpeed);
        }
        else
        {
            Animations.PlayIdle();
        }
    }
}