using UnityEngine;

[CreateAssetMenu(fileName = "FirearmsSO", menuName = "Scriptable Objects/FirearmsSO")]
public class FirearmsSO : WeaponSO<FirearmsInstance>
{
    public GameObject BulletPrefab;
    public int MaxAmmo;
    public int ReserveAmmo;

    public override FirearmsInstance DerivedInstance(Transform hand)
    {
        GameObject weapon = null;
        if (WeaponPrefab != null)
        {
            weapon = Instantiate(WeaponPrefab, hand);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            weapon.SetActive(false);
        }

        FirearmsInstance instance = new FirearmsInstance(this, weapon, MaxAmmo, ReserveAmmo);
        return instance;
    }
}
