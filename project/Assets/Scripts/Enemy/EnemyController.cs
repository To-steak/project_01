using UnityEngine;

[RequireComponent(typeof(EnemyAgent))]
[RequireComponent(typeof(EnemyAnimations))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyWeapon))]
public class EnemyController : MonoBehaviour, IPoolable, INoticeReceiver
{
    [SerializeField] private EnemyConfig config;
    public EnemyConfig Config => config;

    public EnemyAgent Agent { get; private set; }
    public EnemyAnimations Animations { get; private set; }
    public EnemyHealth Health { get; private set; }
    public EnemyWeapon Weapon { get; private set; }
    public EnemyEvents Events { get; private set; }
    public BoxCollider Collider { get; private set; }
#if UNITY_EDITOR
    public EnemyState CurrentState => _currentState;
#endif
    private EnemyState _currentState;
    private GameObject _source;

    public EnemyMoveState Move { get; private set; }
    public EnemyChaseState Chase { get; private set; }
    public EnemyAttackState Attack { get; private set; }
    public EnemyDieState Die { get; private set; }

    public Transform LastTarget { get; private set; }
    public bool HasLastTarget { get; private set; }

    void Awake()
    {
        Move = new EnemyMoveState(this);
        Chase = new EnemyChaseState(this);
        Attack = new EnemyAttackState(this);
        Die = new EnemyDieState(this);

        Agent = GetComponent<EnemyAgent>();
        Animations = GetComponent<EnemyAnimations>();
        Health = GetComponent<EnemyHealth>();
        Weapon = GetComponent<EnemyWeapon>();
        Collider = GetComponent<BoxCollider>();
        Events = new EnemyEvents();
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
#if UNITY_EDITOR
        Weapon.MeleeArea(position);
#endif
        _currentState.Tick();
    }

    public void ChangeState(EnemyState state)
    {
        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void SetSource(GameObject source)
    {
        _source = source;
        Initialize();
    }

    private void Initialize()
    {
        Agent.Initialize(config);
        Animations.Initialize(Events, config);
        Weapon.Initialize();
        Health.Initialize(Events, config);
        Collider.enabled = true;

        _currentState?.Exit();
        _currentState = Move;
        _currentState.Enter();

#if UNITY_EDITOR
        Agent.DrawGizmos(config.MaxDetectRadius, config.AbsoluteDetectRadius, config.AttackRange);
#endif
    }

    public void ReturnPool()
    {
        ObjectPool.Manager.Release(_source, gameObject);
    }

    public void NoticeDamage(Transform transform)
    {
        SetLastTarget(transform);
        _currentState.HandleDamaged(transform);
    }

    public void SetLastTarget(Transform transform)
    {
        LastTarget = transform;
        HasLastTarget = true;
    }

    public void ClearLastTarget()
    {
        HasLastTarget = false;
    }

    // Animation
    private void HandleAnimationFinish() => _currentState.HandleAnimationFinish();
    private void HandleAnimationCommit() => _currentState.HandleAnimationCommit();
    // Game System
    private void HandleDieRequest() => ChangeState(Die);
}
