using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour, IAttack
{
    private bool onCooldown = false;

    public void Attack()
    {
        if (onCooldown) return;
        // vind targets binnen bepaalde range
        Collider[] targets = Physics.OverlapSphere(transform.position, 1.5f);
        foreach (Collider target in targets)
        {
            // pakt playerinvincibility zodat iframes werken
            if (target.TryGetComponent<PlayerInvincibility>(out var player))
            {
                StartCoroutine(AttackCooldown(2));
                player.RemoveHealth(1);
                print("health na hit: " + player.health);
            }
        }
    }

    IEnumerator AttackCooldown(float cooldown)
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }
}
