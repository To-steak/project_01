using UnityEngine;

public class MeleeInstance : WeaponInstance, IPlayerWeapon
{
    public struct Config
    {
        public GameObject WeaponGameObject;
        public Vector3 HalfExtents;
        public LayerMask Layer;
        public float Damage;
        public float AttackSpeed;
    }

    public Vector3 HalfExtents => _half; // use only debug

    private readonly Collider[] _buffer = new Collider[4];
    private Vector3 _half;
    private LayerMask _layer;
    private float _damage;

    public MeleeInstance(Config config) : base(config.WeaponGameObject, config.AttackSpeed)
    {
        _half = config.HalfExtents;
        _layer = config.Layer;
        _damage = config.Damage;
    }

    public PlayerState GetAttackState(PlayerController controller)
    {
        return controller.Swing;
    }

    public override bool Attack(Vector3 position, Transform transform)
    {
        Vector3 direction = (position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        Vector3 center = transform.position + (direction * 1.0f);
        int count = Physics.OverlapBoxNonAlloc(center, _half, _buffer, rotation, _layer);

        for (int i = 0; i < count; i++)
        {
            if (_buffer[i].TryGetComponent<IHealthPoint>(out var component))
            {
                component.TakeDamage(_damage);
            }
        }

        return true;
    }
}