using UnityEngine;
using System.Collections;

public class EnemyLightAttack : MonoBehaviour, IAttack
{
    private bool onCooldown = false;

    public void Attack()
    {
        if (onCooldown) return;

        Vector3 attackPosition = transform.position + transform.forward * 2f;

        // vind targets binnen bepaalde range
        Collider[] targets = Physics.OverlapSphere(attackPosition, 1f);

        foreach (Collider target in targets)
        {
            // pakt playerinvincibility zodat iframes werken
            if (target.TryGetComponent<PlayerInvincibility>(out var player))
            {
                StartCoroutine(AttackCooldown(0.5f));
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
