using System;
using UnityEngine;

public class PlayerEvents
{
    public event Action JumpRequested;

    public void RaiseOnJump() => JumpRequested?.Invoke();
}
