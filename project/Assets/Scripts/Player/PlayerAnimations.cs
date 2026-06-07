using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private int speedHash = Animator.StringToHash("Speed");
    private int jumpHash = Animator.StringToHash("Jump");

    private Animator _animator;
    private PlayerEvents _playerEvents;
    private const float IDLE = 0f;
    private const float WALK = 0.5f;
    private const float RUN = 1.0f;
    private const float DAMPTIME = 0.1f;

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
        _animator.SetFloat(speedHash, IDLE, DAMPTIME, Time.deltaTime);
    }

    public void PlayMove(bool value)
    {
        _animator.SetFloat(speedHash, value ? RUN : WALK, DAMPTIME, Time.deltaTime);
    }

    public void PlayJump()
    {
        _animator.SetTrigger(jumpHash);
    }
}