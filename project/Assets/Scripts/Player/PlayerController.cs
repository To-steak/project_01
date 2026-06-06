using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputs))]
[RequireComponent(typeof(PlayerMovements))]
[RequireComponent(typeof(PlayerAnimations))]
public class PlayerController : MonoBehaviour
{
    public PlayerInputs Inputs => _playerInputs;
    public PlayerMovements Movements => _playerMovements;
    public PlayerAnimations Animations => _playerAnimations;
    public PlayerState State => _playerState; // Only use Debug

    [SerializeField] private PlayerConfig _config;

    private PlayerInputs _playerInputs;
    private PlayerMovements _playerMovements;
    private PlayerAnimations _playerAnimations;
    private PlayerEvents _playerEvents;
    private PlayerState _playerState;
    private Dictionary<System.Type, PlayerState> _states;

    void Awake()
    {
        _playerEvents = new PlayerEvents();
        _states = new Dictionary<System.Type, PlayerState>();

        _states[typeof(PlayerIdleState)] = new PlayerIdleState(this);
        _states[typeof(PlayerMoveState)] = new PlayerMoveState(this);
        // _states[typeof(PlayerJumpState)] = new PlayerJumpState(this);

        _playerState = _states[typeof(PlayerIdleState)];

        if (TryGetComponent<PlayerInputs>(out _playerInputs))
        {
            _playerInputs.Initialize(playerEvents: _playerEvents);
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogError($"PlayerController: PlayerInputs Component is null");
#endif
        }

        if (TryGetComponent<PlayerMovements>(out _playerMovements))
        {
            _playerMovements.Initialize(config: _config, playerEvents: _playerEvents);
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogError($"PlayerController: PlayerMovements Component is null");
#endif
        }

        if (TryGetComponent<PlayerAnimations>(out _playerAnimations))
        {
            _playerAnimations.Initialize(playerEvents: _playerEvents);
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogError($"PlayerController: PlayerAnimations Component is null");
#endif
        }
    }

    void Start()
    {

    }

    void OnEnable()
    {
        _playerEvents.JumpRequested += HandleJumpRequest;
    }

    void OnDisable()
    {
        _playerEvents.JumpRequested -= HandleJumpRequest;
    }

    void Update()
    {
        _playerMovements.Tick();
        _playerState.Tick();
    }

    public void ChangeState<T>() where T : PlayerState
    {
        if (_states.TryGetValue(typeof(T), out var newState))
        {
            _playerState.Exit();
            _playerState = newState;
            _playerState.Enter();
        }
    }

    private void HandleJumpRequest() => _playerState.HandleJump();
}
