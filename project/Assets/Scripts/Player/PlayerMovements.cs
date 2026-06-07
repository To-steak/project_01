using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovements : MonoBehaviour
{
    public bool IsGround { get; private set; }
    public float Speed => _speed; // Only use Debug

    [SerializeField] private Transform model;

    private CharacterController _characterController;
    private PlayerConfig _config;
    private PlayerEvents _playerEvents;
    private Camera _mainCamera;
    private bool _isRotationLocked;
    private float _speed;
    private float _verticalVelocity;
    private const float PRESS = -2f;
    private Vector3 _direction;
    private const float LOOK_SQRT_THRESHOLD = 0.01f;

    public void Initialize(PlayerConfig config, PlayerEvents playerEvents)
    {
        _config = config;
        _playerEvents = playerEvents;
        _mainCamera = Camera.main;
        _isRotationLocked = false;
        _speed = _config.WalkSpeed;

        if (!TryGetComponent<CharacterController>(out _characterController))
        {
#if UNITY_EDITOR
            Debug.LogError($"PlayerMovements: CharacterController Component is null");
#endif
        }
#if UNITY_EDITOR
        Debug.Log($"PlayerMovements.<color=magenta>Initialize</color>");
#endif
    }

    public void Tick()
    {
        Vector3 CheckSphere = transform.position + _config.GroundCheckOffset;
        IsGround = Physics.CheckSphere(CheckSphere, _config.GroundDistance, _config.GroundLayer);

        if (IsGround && _verticalVelocity < 0)
        {
            _verticalVelocity = PRESS;
        }

        _verticalVelocity += _config.Gravity * Time.deltaTime;

        Vector3 direction = GetCameraDirection(_direction);
        Vector3 move = direction * _speed;

        move.y = _verticalVelocity;

        _characterController.Move(move * Time.deltaTime);
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
            _speed = _config.RunSpeed;
        }
        else
        {
            _speed = _config.WalkSpeed;
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
        _speed = _config.DodgeSpeed;
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

        Vector3 groundCheckOffset = Vector3.up * -0.6f;
        float groundDistance = .2f;

        if (_config != null)
        {
            groundCheckOffset = _config.GroundCheckOffset;
            groundDistance = _config.GroundDistance;
        }

        Gizmos.DrawWireSphere(transform.position + groundCheckOffset, groundDistance);
    }
#endif
}
