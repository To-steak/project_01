using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovements : MonoBehaviour
{
    public bool IsGround { get; private set; }
    public Vector3 MouseWorldPosition { get; private set; }

    [SerializeField] private Transform model;

    private CharacterController _characterController;
    private PlayerEvents _playerEvents;
    private Camera _mainCamera;
    private LayerMask _groundLayer;
    private Vector3 _direction;
    private Vector3 _groundCheckOffset = new Vector3(0f, .6f, 0f);
    private bool _isRotationLocked;
    private float _currentSpeed;
    private float _runSpeed;
    private float _walkSpeed;
    private float _dodgeSpeed;
    private float _verticalVelocity;
    private float _gravity;
    private float _groundDistance = 0.2f;
    private const float PRESS = -2f;
    private const float LOOK_SQRT_THRESHOLD = 0.01f;

    public void Initialize(PlayerConfig config, PlayerEvents playerEvents)
    {
        _playerEvents = playerEvents;
        _mainCamera = Camera.main;
        _isRotationLocked = false;
        _walkSpeed = config.WalkSpeed;
        _runSpeed = config.RunSpeed;
        _dodgeSpeed = config.DodgeSpeed;
        _currentSpeed = _walkSpeed;
        _gravity = config.Gravity;
        _groundCheckOffset = config.GroundCheckOffset;
        _groundDistance = config.GroundDistance;
        _groundLayer = config.GroundLayer;

        if (!TryGetComponent<CharacterController>(out _characterController))
        {
#if UNITY_EDITOR
            Debug.LogError($"PlayerMovements: CharacterController Component is null");
#endif
        }
    }

    public void Tick()
    {
        var deltaTime = Time.deltaTime;

        Vector3 CheckSphere = transform.position + _groundCheckOffset;
        IsGround = Physics.CheckSphere(CheckSphere, _groundDistance, _groundLayer);

        if (IsGround && _verticalVelocity < 0)
        {
            _verticalVelocity = PRESS;
        }

        _verticalVelocity += _gravity * deltaTime;

        Vector3 direction = GetCameraDirection(_direction);
        Vector3 move = direction * _currentSpeed;

        move.y = _verticalVelocity;

        _characterController.Move(move * deltaTime);
    }

    public void Look(Vector2 value)
    {
        if (_mainCamera == null)
        {
            return;
        }

        if (!_isRotationLocked)
        {
            Ray ray = _mainCamera.ScreenPointToRay(value);
            Plane plane = new Plane(Vector3.up, new Vector3(0f, transform.position.y, 0));

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 hit = ray.GetPoint(distance);
                MouseWorldPosition = hit;
                Vector3 direction = hit - transform.position;
                direction.y = 0f;

                if (direction.sqrMagnitude > LOOK_SQRT_THRESHOLD)
                {
                    RotateFBX(direction);
                }
            }
        }
    }

    public void SetRunning(bool value)
    {
        if (value)
        {
            _currentSpeed = _runSpeed;
        }
        else
        {
            _currentSpeed = _walkSpeed;
        }
    }

    public void SetDirection(Vector3 input)
    {
        _direction = input;
    }

    public void SetRotationLock(bool value)
    {
        _isRotationLocked = value;
    }

    public void DoDodge()
    {
        _currentSpeed = _dodgeSpeed;
        RotateFBX(GetCameraDirection(_direction));
    }

    private Vector3 GetCameraDirection(Vector3 direction)
    {
        if (_mainCamera == null) return direction;

        Vector3 camForward = _mainCamera.transform.forward;
        Vector3 camRight = _mainCamera.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        return (camForward * direction.z) + (camRight * direction.x);
    }

    private void RotateFBX(Vector3 direction)
    {
        if (model == null)
        {
            return;
        }

        Quaternion rotation = Quaternion.LookRotation(direction);
        Vector3 euler = model.eulerAngles;
        float yaw = rotation.eulerAngles.y;
        rotation = Quaternion.Euler(euler.x, yaw, euler.z);

        model.rotation = rotation;
    }

#if UNITY_EDITOR
    /// <summary>
    /// Only Runtime 
    /// </summary>
    private void OnDrawGizmos()
    {
        if (IsGround)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawWireSphere(transform.position + _groundCheckOffset, _groundDistance);
    }
#endif
}
