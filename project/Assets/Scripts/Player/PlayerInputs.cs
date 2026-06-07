using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public Vector2 Look { get; private set; }
    public Vector2 Move { get; private set; }
    public bool Run { get; private set; }

    private Player_Actions _actions;
    private PlayerEvents _playerEvents;

    void Awake()
    {
        _actions = new Player_Actions();
    }

    void OnEnable()
    {
        _actions.Combat.Enable();

        _actions.Combat.Move.performed += OnMove;
        _actions.Combat.Move.canceled += OnMove;

        _actions.Combat.Run.performed += OnRun;
        _actions.Combat.Run.canceled += OnRun;

        _actions.Combat.Look.performed += OnLook;
        _actions.Combat.Look.canceled += OnLook;

        _actions.Combat.Dodge.performed += OnDodge;

        _actions.Combat.Attack.performed += OnAttack;
        _actions.Combat.Attack.canceled += OnAttack;

        _actions.Combat.Reload.performed += OnReload;
    }

    void OnDisable()
    {
        _actions.Combat.Move.performed -= OnMove;
        _actions.Combat.Move.canceled -= OnMove;

        _actions.Combat.Run.performed -= OnRun;
        _actions.Combat.Run.canceled -= OnRun;

        _actions.Combat.Look.performed -= OnLook;
        _actions.Combat.Look.canceled -= OnLook;

        _actions.Combat.Dodge.performed -= OnDodge;

        _actions.Combat.Attack.performed -= OnAttack;
        _actions.Combat.Attack.canceled -= OnAttack;

        _actions.Combat.Reload.performed -= OnReload;

        _actions.Combat.Disable();
    }

    public void Initialize(PlayerEvents playerEvents)
    {
        _playerEvents = playerEvents;

#if UNITY_EDITOR
        Debug.Log($"PlayerInputs.<color=magenta>Initialize</color>");
#endif
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }

    private void OnRun(InputAction.CallbackContext context)
    {
        Run = context.ReadValueAsButton();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }

    private void OnDodge(InputAction.CallbackContext context)
    {
        var input = context.ReadValueAsButton();
#if UNITY_EDITOR
        Debug.Log($"PlayerInputs.<color=magenta>OnDodge</color>: <color=orange>{input}</color>");
#endif
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        var input = context.ReadValueAsButton();
#if UNITY_EDITOR
        Debug.Log($"PlayerInputs.<color=magenta>OnAttack</color>: <color=orange>{input}</color>");
#endif
    }

    private void OnSwap(InputAction.CallbackContext context)
    {

    }

    private void OnReload(InputAction.CallbackContext context)
    {
        var input = context.ReadValueAsButton();
#if UNITY_EDITOR
        Debug.Log($"PlayerInputs.<color=magenta>OnReload</color>: <color=orange>{input}</color>");
#endif
    }
}
