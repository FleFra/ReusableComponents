using UnityEngine;
using System.Collections;

public class WandererAttack : MonoBehaviour, IAttack
{
    public void Attack()
    {
        // vind targets binnen bepaalde range
        Collider[] targets = Physics.OverlapSphere(transform.position, 1f);
        foreach (Collider target in targets)
        {
            // pakt playerinvincibility
            if (target.TryGetComponent<PlayerInvincibility>(out var player))
            {
                player.RemoveHealth(1);
                print("health na hit: " + player.health);
            }
        }
    }
}
