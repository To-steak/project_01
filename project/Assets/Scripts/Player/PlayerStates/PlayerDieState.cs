using UnityEngine;

public class PlayerDieState : PlayerState
{
    public PlayerDieState(PlayerController playerController) : base(playerController)
    {
    }

    public override void Enter()
    {
        Movements.SetDirection(Vector3.zero);
        Movements.SetRotationLock(true);
        Animations.PlayDie(true);
        Inputs.SetCombatInputEnable(false);
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {

    }
}
