using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private WeaponSO weapon;

    private WeaponInstance instance;

    public void Initialize()
    {
        instance = weapon.Initialize(hand: null);

    }

    public bool TryAttack(Vector3 targetPosition = default)
    {
        return instance.Attack(targetPosition);
    }
}