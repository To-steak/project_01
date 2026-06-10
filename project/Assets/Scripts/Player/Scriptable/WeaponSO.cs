using UnityEngine;

public abstract class WeaponSO : ScriptableObject
{
    public GameObject WeaponPrefab;

    public abstract WeaponInstance Initialize(Transform hand);
}

public abstract class WeaponSO<T> : WeaponSO where T : WeaponInstance
{
    public override WeaponInstance Initialize(Transform hand) => DerivedInitialize(hand);
    public abstract T DerivedInitialize(Transform hand);
}
