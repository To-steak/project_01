using UnityEngine;

public class PlayerAnimations : MonoBehaviour, IAnimationEventReceiver
{
    private readonly int speedHash = Animator.StringToHash("Speed");
    private readonly int dodgeHash = Animator.StringToHash("Dodge");
    private readonly int shotHash = Animator.StringToHash("Shot");
    private readonly int swingHash = Animator.StringToHash("Swing");
    private readonly int swapHash = Animator.StringToHash("Swap");

    private Animator _animator;
    private PlayerEvents _playerEvents;
    private const float IDLE = 0f;
    private const float WALK = 1.0f;
    private const float RUN = 2.0f;

    public void Initialize(PlayerEvents playerEvents)
    {
        _playerEvents = playerEvents;
        _animator = GetComponentInChildren<Animator>();

        if (_animator == null)
        {
#if UNITY_EDITOR
            Debug.LogError($"{gameObject.name}: Animator Component is missing!");
#endif
        }
    }

    public void PlayIdle()
    {
        _animator.SetFloat(speedHash, IDLE);
    }

    public void PlayMove(bool value)
    {
        _animator.SetFloat(speedHash, value ? RUN : WALK);
    }

    public void PlayDodge()
    {
        _animator.SetTrigger(dodgeHash);
    }

    public void PlayShot(bool value)
    {
        _animator.SetBool(shotHash, value);
    }

    public void PlaySwing(bool value)
    {
        _animator.SetBool(swingHash, value);
    }

    public void PlaySwap()
    {
        _animator.SetTrigger(swapHash);
    }

    public void NotifyAnimationFinished() => _playerEvents.RaiseOnAnimationFinish();

    public void NotifyAnimationCommit() => _playerEvents.RaiseOnAnimationCommit();

}