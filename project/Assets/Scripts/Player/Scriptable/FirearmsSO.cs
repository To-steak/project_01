using UnityEngine;

[CreateAssetMenu(fileName = "FirearmsSO", menuName = "Scriptable Objects/FirearmsSO")]
public class FirearmsSO : WeaponSO<FirearmsInstance>
{
    public GameObject BulletPrefab;
    public int MaxAmmo;
    public int ReserveAmmo;

    public override FirearmsInstance DerivedInitialize(Transform hand)
    {
        GameObject weapon = null;
        Muzzle muzzle = null;

        if (WeaponPrefab != null)
        {
            weapon = Instantiate(WeaponPrefab, hand);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            weapon.SetActive(false);

            muzzle = weapon.GetComponentInChildren<Muzzle>();
        }

        FirearmsInstance instance = new FirearmsInstance(weaponPrefab: weapon, bulletPrefab: BulletPrefab, muzzle: muzzle, currentAmmo: MaxAmmo, reserveAmmo: ReserveAmmo);
        return instance;
    }
}
