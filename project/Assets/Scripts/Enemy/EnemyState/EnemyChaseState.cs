using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private float _timer;
    private const float DIRECTION_SQR_THRESHOLD = 0.001f;

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

            if (_timer >= Config.AttackInterval) // 공격 쿨타임이 찼다면
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
                    _controller.transform.rotation = Quaternion.Slerp(_controller.transform.rotation, rotation, Config.RotationSpeed * deltaTime);
                }
            }
        }
        else // 공격 사거리를 벗어났을 때
        {
            Animations.PlayRun(Config.RunAnimSpeed);
            Agent.MoveTo(detected.position, Config.RunSpeed);
        }
    }
}
