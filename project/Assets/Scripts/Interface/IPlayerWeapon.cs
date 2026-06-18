using UnityEngine;

public interface IPlayerWeapon
{
    PlayerState GetAttackState(PlayerController controller);
}
