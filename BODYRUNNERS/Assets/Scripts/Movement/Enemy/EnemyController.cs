using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private IPlayerInput playerInput;
    private IMovement movement;
    private IAttack attack;

    private void Start()
    {
        playerInput = GetComponent<IPlayerInput>();
        movement = GetComponent<IMovement>();
        attack = GetComponent<IAttack>();

        // check of alle components gevonden zijn
        print("playerInput: " + playerInput);
        print("movement: " + movement);
        print("attack: " + attack);
    }

    private void Update()
    {
        Vector3 inputDirection = playerInput.GetMovementInput();
        movement.Move(inputDirection);

        // Altijd naar bewegingsrichting draaien
        if (inputDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(inputDirection.normalized);
        }

        if (playerInput.GetAttackInput())
        {
            attack.Attack();
        }
    }
}
