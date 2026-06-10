using UnityEngine;

public class MeleeInstance : WeaponInstance
{
    public override float AttackSpeed => _attackSpeed;

    private float _attackSpeed;

    public MeleeInstance(GameObject weaponPrefab, float attackSpeed) : base(weaponPrefab)
    {
        _attackSpeed = attackSpeed;
    }

    public override PlayerState GetAttackState(PlayerController controller)
    {
        return controller.Swing;
    }

    public override void Attack(Vector3 targetPosition)
    {
        Debug.Log("Swing");
    }
}