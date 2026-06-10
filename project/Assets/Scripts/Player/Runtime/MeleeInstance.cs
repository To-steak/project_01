using UnityEngine;

public class MeleeInstance : WeaponInstance
{
    public MeleeInstance(WeaponSO weaponSO, GameObject weaponPrefab) : base(weaponSO, weaponPrefab)
    {

    }

    public override PlayerState GetAttackState(PlayerController controller)
    {
        return controller.Swing;
    }

    public override void Attack(Transform muzzle)
    {
        // throw new System.NotImplementedException();
        Debug.Log("Swing");
    }
}