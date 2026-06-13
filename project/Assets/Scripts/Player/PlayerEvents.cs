using System;
using UnityEngine;

public class PlayerEvents
{
    // Input
    public event Action DodgeRequest;
    public event Action<int> SwapRequest;
    public event Action ReloadRequest;
    public void RaiseOnDodge() => DodgeRequest?.Invoke();
    public void RaiseOnSwap(int index) => SwapRequest?.Invoke(index);
    public void RaiseOnReload() => ReloadRequest?.Invoke();
    // Animation
    public event Action AnimationFinishRequest;
    public event Action AnimationCommitRequest;
    public void RaiseOnAnimationFinish() => AnimationFinishRequest?.Invoke();
    public void RaiseOnAnimationCommit() => AnimationCommitRequest?.Invoke();
    // Game System
    public event Action DieRequest;
    public void RaiseOnDie() => DieRequest?.Invoke();
}
