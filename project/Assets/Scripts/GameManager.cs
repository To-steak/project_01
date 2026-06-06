using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    PlayerController controller;
    public TMP_Text CurrentState;

    void Awake()
    {

    }

    void Start()
    {
        var go = Instantiate(player, Vector3.up, Quaternion.identity);
        controller = go.GetComponent<PlayerController>();
    }

    void Update()
    {
        CurrentState.text = $"{controller.State.ToString()}";
    }
}
