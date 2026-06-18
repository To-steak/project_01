using UnityEngine;

public abstract class WeaponInstance
{
    public GameObject WeaponPrefab { get; private set; }
    public abstract float AttackSpeed { get; }

    protected WeaponInstance(GameObject weaponPrefab)
    {
        WeaponPrefab = weaponPrefab;
    }

    public abstract bool Attack(Vector3 targetPosition);
    public virtual bool TryReload() => false;
    public virtual void Reload() { }
}