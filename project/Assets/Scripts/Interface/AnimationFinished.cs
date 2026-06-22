using UnityEngine;

public class AnimationFinished : StateMachineBehaviour
{
    [Range(0, 1)] public float finishTime = 0.95f;

    private IAnimationEventReceiver _receiver;
    private bool _isTriggered;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _isTriggered = false;

        if (_receiver == null)
        {
            _receiver = animator.GetComponentInParent<IAnimationEventReceiver>();
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float loop = stateInfo.normalizedTime % 1.0f;

        if (!_isTriggered && loop >= finishTime)
        {
            _isTriggered = true;
            _receiver?.NotifyAnimationFinished();
        }

        if (_isTriggered && loop < finishTime)
        {
            _isTriggered = false;
        }
    }
}
