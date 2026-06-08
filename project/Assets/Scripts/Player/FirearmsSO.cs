using UnityEngine;

[CreateAssetMenu(fileName = "FirearmsSO", menuName = "Scriptable Objects/FirearmsSO")]
public class FirearmsSO : WeaponSO<FirearmsInstance>
{
    // public GameObject BulletPrefab;
    // public LayerMask TargetLayer;
    public int MaxAmmo;
    public int ReserveAmmo;
    // public float BulletSpeed;

    public override FirearmsInstance DerivedInstance(Transform hand)
    {
        FirearmsInstance instance = new FirearmsInstance(this, MaxAmmo, ReserveAmmo);

        if (WeaponPrefab != null)
        {
            GameObject weapon = Instantiate(WeaponPrefab, hand);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
        }

        return instance;
    }

    public override PlayerState GetDerivedAttackState(PlayerController controller)
    {
        return controller.Shot;
    }
}
