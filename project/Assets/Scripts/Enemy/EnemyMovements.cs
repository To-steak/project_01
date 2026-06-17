using UnityEngine;
using UnityEngine.AI;

public class EnemyMovements : MonoBehaviour
{
    public bool IsMoving
    {
        get
        {
            return _agent.pathPending ||
            _agent.remainingDistance > _agent.stoppingDistance ||
            _agent.velocity.sqrMagnitude > 0.01f;
        }
    }

    public bool HasReached
    {
        get
        {
            if (_agent.pathPending) return false;
            if (!_agent.hasPath) return false;
            return _agent.remainingDistance <= _agent.stoppingDistance &&
            _agent.velocity.sqrMagnitude <= 0.01f;
        }
    }

    private NavMeshAgent _agent;
    private Collider[] _colliders;
    private LayerMask _playerLayer;
    private LayerMask _obstacleLayer;
    private const int MAX_PLAYER = 4;
    private float _interval;
    private float _radius;
    private float _timer;

    public void Initialize(EnemyConfig config)
    {
        _agent = GetComponent<NavMeshAgent>();
        _colliders = new Collider[MAX_PLAYER];
        _playerLayer = config.PlayerLayer;
        _obstacleLayer = config.ObstacleLayer;
        _interval = config.NextMoveInterval;
        _radius = config.MaxDetectRadius;
        _timer = 0;
    }

    public void Tick()
    {
        var deltaTime = Time.deltaTime;
        if (_timer < _interval)
        {
            _timer += deltaTime;
        }

        if (_timer >= _interval)
        {
            Vector3 direction = Random.insideUnitSphere * _radius;
            direction += transform.position;

            if (NavMesh.SamplePosition(direction, out NavMeshHit hit, _radius, NavMesh.AllAreas))
            {
                _agent.SetDestination(hit.position);
            }

            _timer = 0f;
        }
    }

    public Transform DetectPlayer()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders, _playerLayer);

        for (int i = 0; i < count; i++)
        {
            Transform detected = _colliders[i].transform;
            Vector3 direction = (detected.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, detected.position);

            if (!Physics.Raycast(transform.position, direction, distance, _obstacleLayer))
            {
                return detected;
            }
        }

        return null;
    }

    public void ChaseTarget(Transform transform)
    {
        _agent.SetDestination(transform.position);
    }

    public void ResetTimer()
    {
        _timer = 0f;
    }

    public void ResetDestination()
    {
        _agent.SetDestination(transform.position);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, _radius);

        UnityEditor.Handles.color = new Color(1f, 1f, 0f, 0.15f);
        UnityEditor.Handles.DrawSolidDisc(transform.position, Vector3.up, _radius);
    }
#endif
}
