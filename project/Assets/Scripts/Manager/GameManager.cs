using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private CinemachineCamera cinemachine;
    private PlayerController _playerController;
    // private EnemyController _enemyController;

    void Awake()
    {

    }

    void Start()
    {
        _playerController = Instantiate(player, Vector3.up, Quaternion.identity).GetComponent<PlayerController>();

        cinemachine.Follow = _playerController.gameObject.transform;
    }

    void Update()
    {

    }

    public void Summon()
    {
        ObjectPool.Manager.Get(enemy, Vector3.zero, Quaternion.identity);
    }
}
