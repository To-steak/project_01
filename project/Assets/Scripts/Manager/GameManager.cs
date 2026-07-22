using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemyA;
    [SerializeField] private CinemachineCamera cinemachine;
    private PlayerController _playerController;
    // private EnemyController _enemyController;

    void Awake()
    {

    }

    void Start()
    {
        _playerController = Instantiate(player, Vector3.up, Quaternion.identity).GetComponent<PlayerController>();
        GameObject enemy = ObjectPool.Manager.Get(enemyA, Vector3.zero, Quaternion.identity);
        
        cinemachine.Follow = _playerController.gameObject.transform;
    }

    void Update()
    {
        
    }
}
