using UnityEngine;

public interface IAnimationEventReceiver
{
    void NotifyAnimationFinished();
    void NotifyAnimationCommit();
}
