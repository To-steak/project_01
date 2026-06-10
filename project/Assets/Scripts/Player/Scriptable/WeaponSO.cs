using UnityEngine;

public abstract class WeaponSO : ScriptableObject
{
    public GameObject WeaponPrefab;

    public abstract WeaponInstance Instance(Transform hand);
}

public abstract class WeaponSO<T> : WeaponSO where T : WeaponInstance
{
    public override WeaponInstance Instance(Transform hand) => DerivedInstance(hand);
    public abstract T DerivedInstance(Transform hand);
}
