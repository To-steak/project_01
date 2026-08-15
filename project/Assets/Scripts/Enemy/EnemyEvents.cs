using System;
using UnityEngine;

public class EnemyEvents
{
    // Animation
    public event Action AnimationFinishRequest;
    public void RaiseOnAnimationFinish() => AnimationFinishRequest?.Invoke();

    public event Action AnimationCommitRequest;
    public void RaiseOnAnimationCommit() => AnimationCommitRequest?.Invoke();

    // Game System
    public event Action DieRequest;
    public void RaiseOnDie() => DieRequest?.Invoke();
}