using UnityEngine;

public interface IDamageable
{
    public float health {get; set; }
    void RemoveHealth(float amount);
}
