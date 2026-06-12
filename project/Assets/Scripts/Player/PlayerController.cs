using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerInputs))]
[RequireComponent(typeof(PlayerMovements))]
[RequireComponent(typeof(PlayerAnimations))]
[RequireComponent(typeof(PlayerWeapons))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfig _config;

    public PlayerInputs Inputs { get; private set; }
    public PlayerMovements Movements { get; private set; }
    public PlayerAnimations Animations { get; private set; }
    public PlayerWeapons Weapons { get; private set; }
    public PlayerHealth Health { get; private set; }
    public PlayerEvents Events { get; private set; }

    public PlayerState CurrentState => _currentState; // Only use Debug
    private PlayerState _currentState;

    public PlayerIdleState Idle { get; private set; }
    public PlayerMoveState Move { get; private set; }
    public PlayerDodgeState Dodge { get; private set; }
    public PlayerShotState Shot { get; private set; }
    public PlayerSwingState Swing { get; private set; }
    public PlayerSwapState Swap { get; private set; }
    public PlayerReloadState Reload { get; private set; }
    public PlayerThrowState Throw { get; private set; }

    void Awake()
    {
        Idle = new PlayerIdleState(this);
        Move = new PlayerMoveState(this);
        Dodge = new PlayerDodgeState(this);
        Shot = new PlayerShotState(this);
        Swing = new PlayerSwingState(this);
        Swap = new PlayerSwapState(this);
        Reload = new PlayerReloadState(this);
        Throw = new PlayerThrowState(this);

        _currentState = Idle;

        Inputs = GetComponent<PlayerInputs>();
        Movements = GetComponent<PlayerMovements>();
        Animations = GetComponent<PlayerAnimations>();
        Weapons = GetComponent<PlayerWeapons>();
        Health = GetComponent<PlayerHealth>();
        Events = new PlayerEvents();

        Inputs.Initialize(playerEvents: Events);
        Movements.Initialize(config: _config, playerEvents: Events);
        Animations.Initialize(playerEvents: Events);
        Weapons.Initialize();
        Health.Initialize(config: _config);
    }

    void OnEnable()
    {
        // Input
        Events.DodgeRequest += HandleDodgeRequest;
        Events.SwapRequest += HandleSwapRequest;
        Events.ReloadRequest += HandleReloadRequest;
        // Animation
        Events.AnimationFinishRequest += HandleAnimationFinish;
        Events.AnimationCommitRequest += HandleAnimationCommit;
    }

    void OnDisable()
    {
        // Input
        Events.DodgeRequest -= HandleDodgeRequest;
        Events.SwapRequest -= HandleSwapRequest;
        Events.ReloadRequest -= HandleReloadRequest;
        // Animation
        Events.AnimationFinishRequest -= HandleAnimationFinish;
        Events.AnimationCommitRequest -= HandleAnimationCommit;
    }

    void Update()
    {
        _currentState.Tick(); // 1. 입력을 받아서
        Movements.Tick(); // 2. 이동을 먼저 하고
        Movements.Look(Inputs.Look); // 3. 이동 완료한 좌표를 기준으로 Look 계산
        Health.Tick();
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
    private void HandleReloadRequest() => _currentState.HandleReload();
    // Animation
    private void HandleAnimationFinish() => _currentState.HandleAnimationFinish();
    private void HandleAnimationCommit() => _currentState.HandleAnimationCommit();
}
