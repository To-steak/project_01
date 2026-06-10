using UnityEngine;

public abstract class WeaponSO : ScriptableObject
{
    public GameObject WeaponPrefab;
    // public string WeaponName;
    // public float WeaponDamage;

    public abstract WeaponInstance Instance(Transform hand);
    public abstract PlayerState GetAttackState(PlayerController controller);
    public abstract void Attack(Transform muzzle);
}

public abstract class WeaponSO<T> : WeaponSO where T : WeaponInstance
{
    public override WeaponInstance Instance(Transform hand) => DerivedInstance(hand);
    public abstract T DerivedInstance(Transform hand);
    public override PlayerState GetAttackState(PlayerController controller) => GetDerivedAttackState(controller);
    public abstract PlayerState GetDerivedAttackState(PlayerController controller);
    public override void Attack(Transform muzzle) => DerivedAttack(muzzle);
    public abstract void DerivedAttack(Transform muzzle);
}
