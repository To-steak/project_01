using UnityEngine;

public class ThrowingInstance : WeaponInstance
{
    public override float AttackSpeed => _attackSpeed;

    private float _attackSpeed;

    public ThrowingInstance(GameObject weaponPrefab, float attackSpeed) : base(weaponPrefab)
    {
        _attackSpeed = attackSpeed;
    }

    public override void Attack(Vector3 targetPosition)
    {
        Debug.Log("Throw!");
    }

    public override PlayerState GetAttackState(PlayerController controller)
    {
        return controller.Throw;
    }
}
