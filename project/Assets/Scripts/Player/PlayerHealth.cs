using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealthPoint
{
    public float MaxHealth { get; private set; }
    public float MaxMana { get; private set; }
    public float CurrentHealth { get; private set; }
    public float CurrentMana { get; private set; }

    private PlayerEvents _events;
    private float _runCost;
    private float _dodgeCost;
    private float _recoveryManaTimer;
    private float _recoveryManaDelay;
    private float _recoveryManaAmount;

    public void Initialize(PlayerConfig config, PlayerEvents playerEvents)
    {
        MaxHealth = config.MaxHealth;
        MaxMana = config.MaxMana;
        CurrentHealth = config.MaxHealth;
        CurrentMana = config.MaxMana;

        _events = playerEvents;
        _runCost = config.RunCost;
        _dodgeCost = config.DodgeCost;
        _recoveryManaTimer = config.RecoveryManaDelay;
        _recoveryManaDelay = config.RecoveryManaDelay;
        _recoveryManaAmount = config.RecoveryManaAmount;
    }

    public void Tick()
    {
        var deltaTime = Time.deltaTime;

        if (_recoveryManaTimer < _recoveryManaDelay)
        {
            _recoveryManaTimer += deltaTime;
        }

        if (CurrentMana < MaxMana && _recoveryManaTimer >= _recoveryManaDelay)
        {
            CurrentMana += _recoveryManaAmount * deltaTime;
        }
    }

    public bool TryConsumeDodgeMana()
    {
        if (CurrentMana >= _dodgeCost)
        {
            CurrentMana -= _dodgeCost;
            _recoveryManaTimer = 0f;
            return true;
        }

        return false;
    }

    public bool TryConsumeRunMana(float deltaTime)
    {
        float reqMana = _runCost * deltaTime;

        if (CurrentMana >= reqMana)
        {
            CurrentMana -= reqMana;
            _recoveryManaTimer = 0f;
            return true;
        }

        return false;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        Debug.Log($"Player >> damage: {amount}, {CurrentHealth} / {MaxHealth}");
        if (CurrentHealth <= 0)
        {
            Debug.Log("Player is dead");
            _events.RaiseOnDie();
        }
    }

    [ContextMenu("Test Take Damage")]
    public void TestTakeDamage()
    {
        TakeDamage(50);
    }
}
