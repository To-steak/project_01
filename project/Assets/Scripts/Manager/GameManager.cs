using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private CinemachineCamera cinemachine;
    
    public TMP_Text CurrentState;

    private PlayerController _playerController;
    private EnemyController _enemyController;

    void Awake()
    {

    }

    void Start()
    {
        _playerController = Instantiate(player, Vector3.up, Quaternion.identity).GetComponent<PlayerController>();
        _enemyController = Instantiate(enemy, new Vector3(5, 0, 5), Quaternion.identity).GetComponent<EnemyController>();

        cinemachine.Follow = _playerController.gameObject.transform;
    }

    void Update()
    {
        CurrentState.text = $"Current State: {_enemyController.CurrentState}";
    }
}
