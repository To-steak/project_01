using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
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

    public void SetSource(GameObject source)
    {
        _source = source;
    }
}
