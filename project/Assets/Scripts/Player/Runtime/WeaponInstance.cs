using UnityEngine;

public class WeaponInstance
{
    public WeaponSO ScriptableObject { get; private set; }
    public GameObject WeaponPrefab { get; private set; }

    public WeaponInstance(WeaponSO weaponSO, GameObject weaponPrefab)
    {
        ScriptableObject = weaponSO;
        WeaponPrefab = weaponPrefab;
    }
}

public class MeleeInstance : WeaponInstance
{
    public MeleeInstance(WeaponSO weaponSO, GameObject weaponPrefab) : base(weaponSO, weaponPrefab)
    {

    }
}

public class FirearmsInstance : WeaponInstance
{
    public int CurrentAmmo;
    public int ReserveAmmo;

    public FirearmsInstance(WeaponSO weaponSO, GameObject weaponPrefab, int currentAmmo, int reserveAmmo) : base(weaponSO, weaponPrefab)
    {
        CurrentAmmo = currentAmmo;
        ReserveAmmo = reserveAmmo;
    }
}