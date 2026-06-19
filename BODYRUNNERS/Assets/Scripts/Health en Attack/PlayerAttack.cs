using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttack
{
    private bool onCooldown = false;
    private LineRenderer attackRangeCircle;

    private void Start()
    {
        DrawAttackRangeCircle();
    }

    public void Attack()
    {
        // Line omdat unity anders freaked bij attack swap idk
        if (this == null) Destroy(this);

        if (onCooldown) return;
        // vind targets binnen bepaalde range
        Collider[] targets = Physics.OverlapSphere(transform.position, 3);
        
        foreach (Collider target in targets)
        {
            // checkt of de target een enemy is
            if (target.gameObject.GetComponent<EnemyController>()!= null)
            {
                StartCoroutine(AttackCooldown(1));
                target.gameObject.GetComponent<IDamageable>().RemoveHealth(2);
            }
        }
    }

    IEnumerator AttackCooldown(float cooldown)
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    // Methods vanaf hier alleen voor de cone line renderer met dank aan Big C
    private void DrawAttackRangeCircle()
    {
        GameObject lineObj = new GameObject("CircleRenderer");
        lineObj.transform.SetParent(transform);
        lineObj.transform.localPosition = Vector3.zero;

        attackRangeCircle = lineObj.AddComponent<LineRenderer>();
        attackRangeCircle.loop = true;
        attackRangeCircle.useWorldSpace = false;
        attackRangeCircle.widthMultiplier = 0.05f;
        attackRangeCircle.material = new Material(Shader.Find("Sprites/Default"));
        attackRangeCircle.startColor = Color.red;
        attackRangeCircle.endColor = Color.red;

        int segments = 64;
        float radius = 3f;
        float angleStep = 360f / segments;
        attackRangeCircle.positionCount = segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = Mathf.Deg2Rad * angleStep * i;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            attackRangeCircle.SetPosition(i, new Vector3(x, 0f, z));
        }
    }

    private void OnDestroy()
    {
        if (attackRangeCircle != null) Destroy(attackRangeCircle.gameObject);
    }
}
