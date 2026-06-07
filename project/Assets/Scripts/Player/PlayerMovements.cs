using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovements : MonoBehaviour
{
    public bool IsGround { get; private set; }

    private CharacterController _characterController;
    private PlayerConfig _config;
    private PlayerEvents _playerEvents;
    private Camera _mainCamera;
    private float _speed;
    private Vector3 _direction;

    public void Initialize(PlayerConfig config, PlayerEvents playerEvents)
    {
        _config = config;
        _playerEvents = playerEvents;
        _mainCamera = Camera.main;
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

        Vector3 direction = GetCameraDirection(_direction);
        _characterController.Move(direction * _speed * Time.deltaTime);
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

    private Vector3 GetCameraDirection(Vector3 direction)
    {
        if (_mainCamera == null) return direction;

        Vector3 camForward = _mainCamera.transform.forward;
        Vector3 camRight = _mainCamera.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        return (camForward * direction.z) + (camRight * direction.x);
    }

#if UNITY_EDITOR
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
