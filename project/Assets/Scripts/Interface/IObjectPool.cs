using UnityEngine;

public interface IObjectPool
{
    GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation);
    void Release(GameObject source, GameObject instance);
}