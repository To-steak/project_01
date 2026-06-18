using UnityEngine;

public class MeleeInstance : WeaponInstance, IPlayerWeapon
{
    public override float AttackSpeed => _attackSpeed;

    private float _attackSpeed;

    public MeleeInstance(GameObject weaponPrefab, float attackSpeed) : base(weaponPrefab)
    {
        _attackSpeed = attackSpeed;
    }

    public PlayerState GetAttackState(PlayerController controller)
    {
        return controller.Swing;
    }

    public override bool Attack(Vector3 targetPosition)
    {
        Debug.Log("Swing");
        return true;
    }
}