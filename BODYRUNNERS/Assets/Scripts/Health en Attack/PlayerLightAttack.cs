using UnityEngine;
using System.Collections;

public class PlayerLightAttack : MonoBehaviour, IAttack
{
    private bool onCooldown = false;

    // shizzle voor de cone line renderer
    private LineRenderer coneLine;
    private float coneAngle = 45f;
    private float coneRange = 3f;
    private int arcSegments = 20;

    private void Start()
    {
        DrawAttackCone();
    }

    public void Attack()
    {
        // Line omdat unity anders freaked bij attack swap idk
        if (this == null) Destroy(this);

        if (onCooldown) return;
        // vind targets binnen bepaalde range
        Collider[] targets = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z + 2), 1);

        foreach (Collider target in targets)
        {
            // checkt of de target een enemy is
            if (target.gameObject.GetComponent<EnemyController>() != null)
            {
                StartCoroutine(AttackCooldown(0.5f));
                target.gameObject.GetComponent<IDamageable>().RemoveHealth(1);
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
    private void DrawAttackCone()
    {
        GameObject lineObj = new GameObject("ConeRenderer");
        lineObj.transform.SetParent(transform);
        lineObj.transform.localPosition = Vector3.zero;
        lineObj.transform.localRotation = Quaternion.identity;

        coneLine = lineObj.AddComponent<LineRenderer>();
        coneLine.useWorldSpace = false;
        coneLine.widthMultiplier = 0.05f;
        coneLine.material = new Material(Shader.Find("Sprites/Default"));
        coneLine.startColor = Color.yellow;
        coneLine.endColor = Color.yellow;
        coneLine.loop = false;

        coneLine.positionCount = arcSegments + 3;
        coneLine.SetPosition(0, Vector3.zero);

        for (int i = 0; i <= arcSegments; i++)
        {
            float t = (float)i / arcSegments;
            float angle = Mathf.Lerp(-coneAngle / 2, coneAngle / 2, t);
            coneLine.SetPosition(i + 1, GetConePoint(angle));
        }

        coneLine.SetPosition(arcSegments + 2, Vector3.zero);
    }

    private Vector3 GetConePoint(float angleOffset)
    {
        float rad = Mathf.Deg2Rad * angleOffset;
        return new Vector3(Mathf.Sin(rad) * coneRange, 0f, Mathf.Cos(rad) * coneRange);
    }

    private void OnDestroy()
    {
        if (coneLine != null) Destroy(coneLine.gameObject);
    }
}