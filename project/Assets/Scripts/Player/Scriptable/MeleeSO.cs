using UnityEngine;

[CreateAssetMenu(fileName = "MeleeSO", menuName = "Scriptable Objects/MeleeSO")]
public class MeleeSO : WeaponSO<MeleeInstance>
{
    public override MeleeInstance DerivedInstance(Transform hand)
    {
        GameObject weapon = null;
        if (WeaponPrefab != null)
        {
            weapon = Instantiate(WeaponPrefab, hand);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            weapon.SetActive(false);
        }

        MeleeInstance instance = new MeleeInstance(this, weapon);
        return instance;
    }
}
