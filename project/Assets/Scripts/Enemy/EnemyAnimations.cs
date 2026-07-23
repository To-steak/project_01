using UnityEngine;

public class EnemyAnimations : MonoBehaviour, IAnimationEventReceiver
{
    private readonly int speedHash = Animator.StringToHash("Speed");
    private readonly int attackHash = Animator.StringToHash("Attack");
    private readonly int dieHash = Animator.StringToHash("Die");
    private readonly int attackSpeedHash = Animator.StringToHash("AttackSpeed");
    private readonly int animSpeedHash = Animator.StringToHash("AnimSpeed");
    private Animator _animator;
    private EnemyEvents _events;
    private const float IDLE = 0f;
    private const float WALK = 1f;

    public void Initialize(EnemyEvents events, EnemyConfig config)
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.SetFloat(attackSpeedHash, config.AttackSpeed);
        _events = events;
    }

    public void PlayIdle()
    {
        _animator.SetFloat(speedHash, IDLE);
    }

    public void PlayWalk(float value)
    {
        _animator.SetFloat(animSpeedHash, value);
        _animator.SetFloat(speedHash, WALK);
    }

    public void PlayRun(float value)
    {
        _animator.SetFloat(animSpeedHash, value);
        _animator.SetFloat(speedHash, WALK);
    }

    public void PlayAttack()
    {
        _animator.SetTrigger(attackHash);
    }

    public void PlayDie()
    {
        _animator.SetTrigger(dieHash);
    }

    public void NotifyAnimationFinished() => _events.RaiseOnAnimationFinish();
    public void NotifyAnimationCommit() => _events.RaiseOnAnimationCommit();

    public float GetAttackSpeedDebug()
    {
        return _animator.GetFloat(attackSpeedHash);
    }
}
