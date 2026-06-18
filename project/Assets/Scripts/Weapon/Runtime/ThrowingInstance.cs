using UnityEngine;

public class ThrowingInstance : WeaponInstance, IPlayerWeapon
{
    public override float AttackSpeed => _attackSpeed;

    private float _attackSpeed;

    public ThrowingInstance(GameObject weaponPrefab, float attackSpeed) : base(weaponPrefab)
    {
        _attackSpeed = attackSpeed;
    }

    public override bool Attack(Vector3 targetPosition)
    {
        Debug.Log("Throw!");
        return true;
    }

    public PlayerState GetAttackState(PlayerController controller)
    {
        return controller.Throw;
    }
}
