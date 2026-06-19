using System.Collections;
using UnityEngine;

public class PlayerInvincibility : MonoBehaviour, IDamageable
{
    [SerializeField] private float invincibilityDuration = 0.5f;

    private TakeDamage takeDamage;
    private bool isInvincible = false;

    public float health
    {
        get => takeDamage.health;
        set => takeDamage.health = value;
    }

    private void Start()
    {
        takeDamage = GetComponent<TakeDamage>();
    }

    public void RemoveHealth(float amount)
    {
        // skip damage als de speler al geraakt is
        if (isInvincible) return;
        takeDamage.RemoveHealth(amount);
        StartCoroutine(InvincibilityFrames());
    }

    private IEnumerator InvincibilityFrames()
    {
        // zet invincibility aan en wacht dan uit
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }
}