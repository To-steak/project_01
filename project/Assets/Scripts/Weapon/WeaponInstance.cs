using UnityEngine;

public abstract class WeaponInstance
{
    public GameObject WeaponGameObject { get; private set; }
    public float AttackSpeed { get; }

    protected WeaponInstance(GameObject weaponPrefab, float attackSpeed)
    {
        WeaponGameObject = weaponPrefab;
        AttackSpeed = attackSpeed;
    }

    public abstract bool Attack(Vector3 position = default, Transform transform = default);
    public virtual bool IsReloadableWeapon() => false;
    public virtual void Reload() { }
}