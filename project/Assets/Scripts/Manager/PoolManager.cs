using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour, IObjectPool
{
    [System.Serializable]
    public struct Pool
    {
        public GameObject Prefab;
        [Tooltip("Prefab Initial Generation Count")]
        public int Count;
    }

    [SerializeField] private Pool[] bulletPools;
    [SerializeField] private Pool[] enemyPools;

    /// <summary>
    /// GameObject Prefab을 Key로 사용해 반환할 Pool을 식별한다.
    /// Get() / Release() 시그니처의 prefab/source 인자가 이 Key 역할을 한다.
    /// 
    /// 주의: Get() 내부에서 SetSource()에 넘기는 값은 반드시 prefab이어야 한다.
    /// instance를 넘기면 풀링이 깨진 채로 조용히 동작한다.
    /// (KeyNotFound 없이 매번 새 Pool이 생성되어 재사용이 전혀 안 됨).
    /// </summary>
    private readonly Dictionary<GameObject, Queue<GameObject>> _pools = new();

    void Awake()
    {
        ObjectPool.Manager = this;

        foreach (var pool in bulletPools)
        {
            var queue = new Queue<GameObject>();
            for (int i = 0; i < pool.Count; i++)
            {
                GameObject prefab = Instantiate(pool.Prefab, transform);
                prefab.SetActive(false);
                queue.Enqueue(prefab);
            }

            _pools[pool.Prefab] = queue;
        }

        foreach (var pool in enemyPools)
        {
            var queue = new Queue<GameObject>();
            for (int i = 0; i < pool.Count; i++)
            {
                GameObject prefab = Instantiate(pool.Prefab, transform);
                prefab.SetActive(false);
                queue.Enqueue(prefab);
            }

            _pools[pool.Prefab] = queue;
        }
    }

    public GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!_pools.TryGetValue(prefab, out var queue))
        {
            queue = new Queue<GameObject>();
            _pools[prefab] = queue;
        }

        GameObject instance = queue.Count > 0 ? queue.Dequeue() : Instantiate(prefab, transform);

        instance.transform.SetPositionAndRotation(position, rotation);
        instance.SetActive(true);

        if (instance.TryGetComponent<IPoolable>(out var poolable))
        {
            poolable.SetSource(prefab);
        }
        return instance;
    }

    public void Release(GameObject source, GameObject instance)
    {
        instance.gameObject.SetActive(false);

        if (!_pools.TryGetValue(source, out var queue))
        {
            Debug.LogError($"Release: 등록되지 않은 source({source})로 반환 시도, 대상 {instance}");
            Destroy(instance);
            return;
        }
        queue.Enqueue(instance);
    }
}
