using UnityEngine;

public class MeleeInstance : WeaponInstance
{
    public MeleeInstance(GameObject weaponPrefab) : base(weaponPrefab)
    {

    }

    public override PlayerState GetAttackState(PlayerController controller)
    {
        return controller.Swing;
    }

    public override void Attack(Vector3 targetPosition)
    {
        Debug.Log("Swing");
    }
}