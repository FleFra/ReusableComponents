using UnityEngine;
using UnityEngine.AI;

public class EnemyInput : MonoBehaviour, IPlayerInput
{
    Transform target;
    NavMeshAgent agent;

    private void Start()
    {
        target = FindAnyObjectByType<PlayerController>().gameObject.transform;
        agent = GetComponent<NavMeshAgent>();
        // Zorg dat navmesh agent zichzelf niet beweegt 
        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    private void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 15f);

        foreach (Collider hit in hits)
        {
            if (hit.gameObject.TryGetComponent<PlayerInvincibility>(out var player))
            {
                if (target != null)
                {
                    agent.SetDestination(target.position);
                    Vector3 direction = target.position - transform.position;
                    direction.y = 0f;

                    if (direction.sqrMagnitude > 0.01f)
                    {
                        transform.rotation = Quaternion.LookRotation(direction);
                    }
                }
            }
        }
    }

    public Vector3 GetMovementInput()
    {
        // Returned de volgende stap die de agent moet nemeen om naar zijn target te gaan 
        // -transform.position omdat hij anders in worldspace beweegt en helemaal freaked
        if (agent.path.corners.Length < 2) return Vector3.zero;
        return agent.path.corners[1]-transform.position;
    }

    public bool GetAttackInput()
    {
        return true;
    }
}
