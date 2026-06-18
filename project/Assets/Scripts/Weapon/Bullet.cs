using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 10f;
    private float _timer = 0f;
    private const float LIFE_TIME = 5f;

    void Start()
    {

    }

    void Update()
    {
        var deltaTime = Time.deltaTime;
        transform.Translate(Vector3.forward * _speed * deltaTime);

        _timer += deltaTime;
        if (_timer >= LIFE_TIME)
        {
            gameObject.SetActive(false);
        }
    }
}
