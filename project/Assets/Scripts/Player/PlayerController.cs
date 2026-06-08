using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerInputs))]
[RequireComponent(typeof(PlayerMovements))]
[RequireComponent(typeof(PlayerAnimations))]
[RequireComponent(typeof(PlayerWeapons))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfig _config;

    public PlayerInputs Inputs { get; private set; }
    public PlayerMovements Movements { get; private set; }
    public PlayerAnimations Animations { get; private set; }
    public PlayerWeapons Weapons { get; private set; }
    public PlayerEvents Events { get; private set; }

    public PlayerState CurrentState => _currentState; // Only use Debug
    private PlayerState _currentState;

    public PlayerIdleState Idle { get; private set; }
    public PlayerMoveState Move { get; private set; }
    public PlayerDodgeState Dodge { get; private set; }
    public PlayerShotState Shot { get; private set; }
    public PlayerSwingState Swing { get; private set; }
    
    void Awake()
    {
        Idle = new PlayerIdleState(this);
        Move = new PlayerMoveState(this);
        Dodge = new PlayerDodgeState(this);
        Shot = new PlayerShotState(this);
        Swing = new PlayerSwingState(this);

        _currentState = Idle;

        Inputs = GetComponent<PlayerInputs>();
        Movements = GetComponent<PlayerMovements>();
        Animations = GetComponent<PlayerAnimations>();
        Weapons = GetComponent<PlayerWeapons>();
        Events = new PlayerEvents();

        Inputs.Initialize(playerEvents: Events);
        Movements.Initialize(config: _config, playerEvents: Events);
        Animations.Initialize(playerEvents: Events);
        Weapons.Initialize();
    }

    void OnEnable()
    {
        Events.DodgeRequest += HandleDodgeRequest;
        Events.SwapRequest += HandleSwapRequest;
        Events.AnimationFinishRequest += HandleAnimationFinish;
    }

    void OnDisable()
    {
        Events.DodgeRequest -= HandleDodgeRequest;
        Events.SwapRequest -= HandleSwapRequest;
        Events.AnimationFinishRequest -= HandleAnimationFinish;
    }

    void Update()
    {
        _currentState.Tick(); // 1. 입력을 받아서
        Movements.Tick(); // 2. 이동을 먼저 하고
        Movements.Look(Inputs.Look); // 3. 이동 완료한 좌표를 기준으로 Look 계산
    }

    public void ChangeState(PlayerState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    // Input
    private void HandleDodgeRequest() => _currentState.HandleDodge();
    private void HandleSwapRequest(int index) => _currentState.HandleSwap(index);
    // Animation
    private void HandleAnimationFinish() => _currentState.HandleAnimationFinish();
}
