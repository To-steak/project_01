using UnityEngine;

public class ThrowingInstance : WeaponInstance
{
    public ThrowingInstance(GameObject weaponPrefab, float attackSpeed) : base(weaponPrefab, attackSpeed)
    {
    }

    public override bool Attack(Vector3 position, Transform transform)
    {
        Debug.Log("Throw!");
        return true;
    }
}
