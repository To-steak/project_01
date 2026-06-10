using UnityEngine;

public abstract class WeaponInstance
{
    public WeaponSO ScriptableObject { get; private set; }
    public GameObject WeaponPrefab { get; private set; }

    public WeaponInstance(WeaponSO weaponSO, GameObject weaponPrefab)
    {
        ScriptableObject = weaponSO;
        WeaponPrefab = weaponPrefab;
    }
    
    public abstract PlayerState GetAttackState(PlayerController controller);
    public abstract void Attack(Transform muzzle);
}