using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private WeaponSO[] weapons;
    [SerializeField] private Transform hand;
    [SerializeField] private Transform muzzle;

    private WeaponInstance[] instances;
    private int _currentWeaponIndex;

    public void Initialize()
    {
        instances = new WeaponInstance[weapons.Length];
        _currentWeaponIndex = 0;

        for (int i = 0; i < weapons.Length; i++)
        {
            instances[i] = weapons[i].Instance(hand: hand);
        }

        instances[_currentWeaponIndex].WeaponPrefab.SetActive(true);
    }

    public PlayerState GetAttackState(PlayerController controller)
    {
        return instances[_currentWeaponIndex].GetAttackState(controller);
    }

    public bool TrySwapWeapon(int index)
    {
        if (index == _currentWeaponIndex)
        {
            return false;
        }

        if (weapons[index] == null)
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

    public void Attack()
    {
        instances[_currentWeaponIndex].Attack(muzzle);
    }
}
