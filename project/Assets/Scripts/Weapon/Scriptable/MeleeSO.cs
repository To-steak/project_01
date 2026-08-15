using UnityEngine;

[CreateAssetMenu(fileName = "MeleeSO", menuName = "Scriptable Objects/MeleeSO")]
public class MeleeSO : WeaponSO<MeleeInstance>
{
    public float Damage;
    public LayerMask Layer;
    public Vector3 Half;

    public override MeleeInstance DerivedInitialize(Transform hand)
    {
        GameObject weapon = null;

        if (WeaponPrefab != null)
        {
            weapon = Instantiate(WeaponPrefab, hand);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            weapon.SetActive(false);
        }

        MeleeInstance.Config config = new MeleeInstance.Config()
        {
            WeaponGameObject = weapon,
            HalfExtents = Half,
            Layer = Layer,
            Damage = Damage,
            AttackSpeed = AttackSpeed
        };

        MeleeInstance instance = new MeleeInstance(config);
        return instance;
    }
}
