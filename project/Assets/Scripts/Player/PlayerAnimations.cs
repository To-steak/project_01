using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private int walkHash = Animator.StringToHash("Walk");
    private int runHash = Animator.StringToHash("Run");
    private int jumpHash = Animator.StringToHash("Jump");

    private Animator _animator;
    private PlayerEvents _playerEvents;

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
        _animator.Play("Idle");
    }

    public void PlayWalk(bool value)
    {
        _animator.SetBool(walkHash, value);

    }

    public void PlayRun(bool value)
    {
        _animator.SetBool(runHash, value);
    }

    public void PlayJump()
    {
        _animator.SetTrigger(jumpHash);
    }
}