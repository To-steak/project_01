using UnityEngine;

public abstract class WeaponInstance
{
    public GameObject WeaponPrefab { get; private set; }
    public abstract float AttackSpeed { get; }

    public WeaponInstance(GameObject weaponPrefab)
    {
        WeaponPrefab = weaponPrefab;
    }

    public abstract PlayerState GetAttackState(PlayerController controller);
    public abstract void Attack(Vector3 targetPosition);
    public virtual bool TryReload() => false;
    public virtual void Reload() { }
}