using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TMP_Text CurrentState;
    public TMP_Text CurrentSpeed;

    [SerializeField] private GameObject player;
    [SerializeField] private CinemachineCamera cinemachine;

    private PlayerController _controller;

    void Awake()
    {

    }

    void Start()
    {
        _controller = Instantiate(player, Vector3.up, Quaternion.identity).GetComponent<PlayerController>();

        cinemachine.Follow = _controller.gameObject.transform;
    }

    void Update()
    {
        CurrentState.text = $"Current State: {_controller.CurrentState}";
        CurrentSpeed.text = $"Current Speed: {_controller.Movements.Speed}";
    }
}
