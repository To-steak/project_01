using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealthPoint
{
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }

    private EnemyEvents _events;

    public void Initialize(EnemyEvents events, EnemyConfig config)
    {
        MaxHealth = config.InitHealth;
        CurrentHealth = config.InitHealth;

        _events = events;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        Debug.Log($"Enemy >> damage: {amount}, {CurrentHealth} / {MaxHealth}");
        if (CurrentHealth <= 0)
        {
            Debug.Log("Enemy is dead");
            _events.RaiseOnDie();
        }
    }

    [ContextMenu("Test Take Damage")]
    public void TestTakeDamage()
    {
        TakeDamage(50);
    }
}
