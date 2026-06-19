using UnityEngine;
using UnityEngine.AI;

public class WandererInput : MonoBehaviour, IPlayerInput
{
    private NavMeshAgent agent;

    [SerializeField] private float wanderRadius = 100f;
    [SerializeField] private float arriveDistance = 1f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updatePosition = false;
        agent.updateRotation = false;

        PickNewDestination();
    }

    private void Update()
    {
        // Nieuw doel kiezen als we bijna aangekomen zijn
        if (!agent.pathPending && agent.remainingDistance <= arriveDistance)
        {
            PickNewDestination();
        }
    }

    private void PickNewDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection.y += 2;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    public Vector3 GetMovementInput()
    {
        if (agent.path.corners.Length < 2)
            return Vector3.zero;

        return agent.path.corners[1] - transform.position;
    }

    public bool GetAttackInput()
    {
        return true;
    }
}