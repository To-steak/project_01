using UnityEngine;

public class WeaponInstance
{
    public WeaponSO ScriptableObject { get; private set; }
    
    public WeaponInstance(WeaponSO weapon)
    {
        ScriptableObject = weapon;
    }
}

public class MeleeInstance : WeaponInstance
{
    public MeleeInstance(WeaponSO weapon) : base(weapon)
    {

    }
}

public class FirearmsInstance : WeaponInstance
{
    public int CurrentAmmo;
    public int ReserveAmmo;

    public FirearmsInstance(WeaponSO weapon, int currentAmmo, int reserveAmmo) : base(weapon)
    {
        CurrentAmmo = currentAmmo;
        ReserveAmmo = reserveAmmo;
    }
}