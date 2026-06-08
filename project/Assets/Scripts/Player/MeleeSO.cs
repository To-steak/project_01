using UnityEngine;

[CreateAssetMenu(fileName = "MeleeSO", menuName = "Scriptable Objects/MeleeSO")]
public class MeleeSO : WeaponSO<MeleeInstance>
{
    public override MeleeInstance DerivedInstance(Transform hand)
    {
        MeleeInstance instance = new MeleeInstance(this);

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
        return controller.Swing;
    }
}
