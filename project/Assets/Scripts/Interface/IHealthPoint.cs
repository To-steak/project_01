using UnityEngine;

public interface IHealthPoint
{
    float MaxHealth { get; }
    float CurrentHealth { get; }
    void TakeDamage(float amount);
}
