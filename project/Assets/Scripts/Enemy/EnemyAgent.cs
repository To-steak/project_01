using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour
{
    public bool IsMoving => _agent.pathPending || _agent.remainingDistance > _agent.stoppingDistance || _agent.velocity.sqrMagnitude > VELOCITY_SQR_THRESHOLD;

    private NavMeshAgent _agent;
    private Collider[] _detected;
    private const int MAX_PLAYER = 4;
    private const float VELOCITY_SQR_THRESHOLD = 0.01f;
    private float _radius; // debug only
    private float _attackRange; // debug only

    public void Initialize(EnemyConfig config)
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.stoppingDistance = config.AttackRange;
        _detected = new Collider[MAX_PLAYER];
    }

    public Transform DetectPlayer(Vector3 origin, float radius, LayerMask player, LayerMask obstacle)
    {
        int count = Physics.OverlapSphereNonAlloc(origin, radius, _detected, player);

        for (int i = 0; i < count; i++)
        {
            Transform detected = _detected[i].transform;
            Vector3 direction = (detected.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, detected.position);

            if (!Physics.Raycast(origin, direction, distance, obstacle))
            {
                return detected;
            }
        }

        return null;
    }

    public bool TryGetRandomDestination(Vector3 origin, float min, float max, out Vector3 result)
    {
        Vector3 direction = Random.onUnitSphere;
        direction.y = 0f;
        direction.Normalize();

        float distance = Random.Range(min, max);
        Vector3 position = origin + (direction * distance);

        if (NavMesh.SamplePosition(position, out NavMeshHit hit, max, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    public bool TryReachedTarget(Vector3 target, Vector3 origin)
    {
        var range = _agent.stoppingDistance;
        float distance = (target - origin).sqrMagnitude;
        return distance <= (range * range);
    }

    public void MoveTo(Vector3 destination, float speed)
    {
        _agent.speed = speed;
        _agent.SetDestination(destination);
    }

    public void Stop()
    {
        _agent.velocity = Vector3.zero;
        _agent.ResetPath();
    }

#if UNITY_EDITOR
    public void DrawGizmos(float radius, float attackRange)
    {
        _radius = radius;
        _attackRange = attackRange;
    }

    private void OnDrawGizmos()
    {
        // 감지 사거리
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, _radius);

        UnityEditor.Handles.color = new Color(1f, 1f, 0f, 0.15f);
        UnityEditor.Handles.DrawSolidDisc(transform.position, Vector3.up, _radius);

        // 공격 사거리
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, _attackRange);

        UnityEditor.Handles.color = new Color(1f, 0f, 0f, 0.15f);
        UnityEditor.Handles.DrawSolidDisc(transform.position, Vector3.up, _attackRange);
    }
#endif
}
