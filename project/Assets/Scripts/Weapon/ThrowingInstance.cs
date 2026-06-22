using UnityEngine;

public class ThrowingInstance : WeaponInstance, IPlayerWeapon
{
    public ThrowingInstance(GameObject weaponPrefab, float attackSpeed) : base(weaponPrefab, attackSpeed)
    {
    }

    public override bool Attack(Vector3 position, Transform transform)
    {
        Debug.Log("Throw!");
        return true;
    }

    public PlayerState GetAttackState(PlayerController controller)
    {
        return controller.Throw;
    }
}
