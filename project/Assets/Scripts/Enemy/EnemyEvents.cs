using System;

public class EnemyEvents
{
    // Animation
    public event Action AnimationFinishRequest;
    public event Action AnimationCommitRequest;
    public void RaiseOnAnimationFinish() => AnimationFinishRequest?.Invoke();
    public void RaiseOnAnimationCommit() => AnimationCommitRequest?.Invoke();
    // Game System
    public event Action DieRequest;
    public void RaiseOnDie() => DieRequest?.Invoke();
}