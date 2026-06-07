using System;
using UnityEngine;

public class PlayerEvents
{
    public event Action DodgeRequest;
    public void RaiseOnDodge() => DodgeRequest?.Invoke();

    public event Action AnimationFinishRequest;
    public void RaiseOnAnimationFinish() => AnimationFinishRequest?.Invoke();
}
