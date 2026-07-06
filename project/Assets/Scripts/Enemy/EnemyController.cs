using UnityEngine;

[RequireComponent(typeof(EnemyAgent))]
[RequireComponent(typeof(EnemyAnimations))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyWeapon))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyConfig config;
    public EnemyConfig Config => config;

    public EnemyAgent Agent { get; private set; }
    public EnemyAnimations Animations { get; private set; }
    public EnemyHealth Health { get; private set; }
    public EnemyWeapon Weapon { get; private set; }
    public EnemyEvents Events { get; private set; }

    public EnemyState CurrentState => _currentState; // Debug Only
    private EnemyState _currentState;

    public EnemyMoveState Move { get; private set; }
    public EnemyChaseState Chase { get; private set; }
    public EnemyAttackState Attack { get; private set; }
    public EnemyDieState Die { get; private set; }

    void Awake()
    {
        Move = new EnemyMoveState(this);
        Chase = new EnemyChaseState(this);
        Attack = new EnemyAttackState(this);
        Die = new EnemyDieState(this);

        _currentState = Move;

        Agent = GetComponent<EnemyAgent>();
        Animations = GetComponent<EnemyAnimations>();
        Health = GetComponent<EnemyHealth>();
        Weapon = GetComponent<EnemyWeapon>();
        Events = new EnemyEvents();

        Agent.Initialize(config);
        Animations.Initialize(events: Events, config: config);
        Weapon.Initialize();
        Health.Initialize(Events, config);

        Agent.DrawGizmos(config.MaxDetectRadius, config.AbsoluteDetectRadius, config.AttackRange); // Only Debug
    }

    void OnEnable()
    {
        // Animation
        Events.AnimationFinishRequest += HandleAnimationFinish;
        Events.AnimationCommitRequest += HandleAnimationCommit;
        // Game System
        Events.DieRequest += HandleDieRequest;
    }

    void OnDisable()
    {
        // Animation
        Events.AnimationFinishRequest -= HandleAnimationFinish;
        Events.AnimationCommitRequest -= HandleAnimationCommit;
        // Game System
        Events.DieRequest -= HandleDieRequest;
    }

    void Update()
    {
        Vector3 position = transform.position + (transform.forward * 1);
        // position += transform.forward;

        Weapon.MeleeArea(position); // Debug

        _currentState.Tick();
    }

    public void ChangeState(EnemyState state)
    {
        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    // Animation
    private void HandleAnimationFinish() => _currentState.HandleAnimationFinish();
    private void HandleAnimationCommit() => _currentState.HandleAnimationCommit();
    // Game System
    private void HandleDieRequest() => ChangeState(Die);
}
