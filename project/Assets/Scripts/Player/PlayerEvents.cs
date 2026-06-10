using System;
using UnityEngine;

public class PlayerEvents
{
    public event Action DodgeRequest;
    public void RaiseOnDodge() => DodgeRequest?.Invoke();

    public event Action<int> SwapRequest;
    public void RaiseOnSwap(int index) => SwapRequest?.Invoke(index);

    public event Action ReloadRequest;
    public void RaiseOnReload() => ReloadRequest?.Invoke();

    public event Action AnimationFinishRequest;
    public void RaiseOnAnimationFinish() => AnimationFinishRequest?.Invoke();

    public event Action AnimationCommitRequest;
    public void RaiseOnAnimationCommit() => AnimationCommitRequest?.Invoke();
}
