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

        _playerState = _states[typeof(PlayerIdleState)];

        if (TryGetComponent<PlayerInputs>(out _playerInputs))
        {
            _playerInputs.Initialize(playerEvents: _playerEvents);
        }

        if (TryGetComponent<PlayerMovements>(out _playerMovements))
        {
            _playerMovements.Initialize(config: _config, playerEvents: _playerEvents);
        }

        if (TryGetComponent<PlayerAnimations>(out _playerAnimations))
        {
            _playerAnimations.Initialize(playerEvents: _playerEvents);
        }
    }

    void Start()
    {

    }

    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

    void Update()
    {
        _playerState.Tick();                            // 1. 입력을 받아서
        _playerMovements.Tick();                        // 2. 이동을 먼저 하고
        _playerMovements.Look(_playerInputs.Look);      // 3. 이동 완료한 좌표를 기준으로 Look 계산
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
}
