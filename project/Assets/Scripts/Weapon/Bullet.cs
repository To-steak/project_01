using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _damage;
    private GameObject _source;
    private float _speed = 10f;
    private float _timer = 0f;
    private const float LIFE_TIME = 5f;

    void OnEnable()
    {
        _timer = 0f;
    }

    void Update()
    {
        var deltaTime = Time.deltaTime;
        transform.Translate(Vector3.forward * _speed * deltaTime);

        _timer += deltaTime;
        if (_timer >= LIFE_TIME)
        {
            ObjectPool.Manager.Release(_source, gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((_targetLayer.value & (1 << other.gameObject.layer)) != 0 && other.TryGetComponent<IHealthPoint>(out var target))
        {
            target.TakeDamage(_damage);
        }

        ObjectPool.Manager.Release(_source, gameObject);
    }

    public void SetSource(GameObject source)
    {
        _source = source;
    }
}
