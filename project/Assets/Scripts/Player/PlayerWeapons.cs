using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private WeaponSO[] weapons;
    [SerializeField] private Transform hand;
    [SerializeField] private Transform muzzle;

    private WeaponInstance[] instances;

    public void Initialize()
    {
        instances = new WeaponInstance[weapons.Length];

        for (int i = 0; i < weapons.Length; i++)
        {
            instances[i] = weapons[i].Instance(hand: hand);
        }
    }

    public PlayerState GetAttackState(PlayerController controller)
    {
        return weapons[0].GetAttackState(controller);
    }
}
