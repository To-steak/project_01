using UnityEngine;

public class FirearmsInstance : WeaponInstance
{
    public int CurrentAmmo;
    public int ReserveAmmo;

    public FirearmsInstance(WeaponSO weaponSO, GameObject weaponPrefab, int currentAmmo, int reserveAmmo) : base(weaponSO, weaponPrefab)
    {
        CurrentAmmo = currentAmmo;
        ReserveAmmo = reserveAmmo;
    }

    public override PlayerState GetAttackState(PlayerController controller)
    {
        return controller.Shot;
    }

    public override void Attack(Transform muzzle)
    {
        // throw new System.NotImplementedException();
        Debug.Log("Shot");
    }
}