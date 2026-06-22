using UnityEngine;

[CreateAssetMenu(fileName = "ThrowableSO", menuName = "Scriptable Objects/ThrowableSO")]
public class ThrowableSO : WeaponSO<ThrowingInstance>
{
    public int ReserveGrenade;

    public override ThrowingInstance DerivedInitialize(Transform hand)
    {
        GameObject weapon = null;

        if (WeaponPrefab != null)
        {
            weapon = Instantiate(WeaponPrefab, hand);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            weapon.SetActive(false);
        }

        ThrowingInstance instance = new ThrowingInstance(weapon, AttackSpeed);
        return instance;
    }
}
