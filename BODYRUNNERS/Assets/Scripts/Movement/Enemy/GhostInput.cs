using UnityEngine;
using UnityEngine.AI;

public class GhostInput : MonoBehaviour, IPlayerInput
{
    Transform target;

    private void Start()
    {
        target = FindAnyObjectByType<PlayerController>().gameObject.transform;
    }

    public Vector3 GetMovementInput()
    {
        return target.position - transform.position;
    }

    public bool GetAttackInput()
    {
        return true;
    }
}
