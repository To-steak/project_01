using UnityEngine;

public class PlayerWeapons : MonoBehaviour, IPlayerWeapon
{
    public float AttackSpeed => instances[_currentWeaponIndex].AttackSpeed;

    [SerializeField] private WeaponSO[] weapons;
    [SerializeField] private Transform hand;

    private WeaponInstance[] instances;
    private int _currentWeaponIndex;

    public void Initialize()
    {
        var weaponCount = weapons.Length;
        instances = new WeaponInstance[weaponCount];
        _currentWeaponIndex = 0;

        for (int i = 0; i < weaponCount; i++)
        {
            instances[i] = weapons[i].Initialize(hand: hand);
        }

        instances[_currentWeaponIndex].WeaponPrefab.SetActive(true);
    }

    public PlayerState GetAttackState(PlayerController controller)
    {
        return (instances[_currentWeaponIndex] as IPlayerWeapon).GetAttackState(controller);
    }

    public bool TrySwapWeapon(int index)
    {
        if (index == _currentWeaponIndex)
        {
            return false;
        }

        if (instances[index] == null)
        {
            return false;
        }

        var before = instances[_currentWeaponIndex];
        before.WeaponPrefab.SetActive(false);

        _currentWeaponIndex = index;

        var after = instances[_currentWeaponIndex];
        after.WeaponPrefab.SetActive(true);

        return true;
    }

    public bool TryAttack(Vector3 targetPosition = default)
    {
        return instances[_currentWeaponIndex].Attack(targetPosition);
    }

    public bool TryReload()
    {
        return instances[_currentWeaponIndex].TryReload();
    }

    public void Reload()
    {
        instances[_currentWeaponIndex].Reload();
    }
}
