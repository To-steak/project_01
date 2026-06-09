using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerController playerController) : base(playerController)
    {

    }

    public override void Enter()
    {
        Movements.SetDirection(Vector3.zero);
        Animations.PlayIdle();
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        Vector3 input = Inputs.Move;
        if (input != Vector3.zero)
        {
            _controller.ChangeState(_controller.Move);
            return;
        }

        bool attack = Inputs.Attack;
        if (attack)
        {
            var state = Weapons.GetAttackState(_controller);
            _controller.ChangeState(state);
            return;
        }
    }

    public override void HandleSwap(int index)
    {
        if (Movements.IsGround)
        {
            if (Weapons.TrySwapWeapon(index))
            {
                _controller.ChangeState(_controller.Swap);
            }
        }
    }
}