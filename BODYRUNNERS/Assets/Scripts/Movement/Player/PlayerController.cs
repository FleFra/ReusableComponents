using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IPlayerInput playerInput;
    private IMovement movement;
    private IAttack attack;

    private void Start()
    {
        gameObject.AddComponent<PlayerLightAttack>();
        Initialise();
    }

    private void Update()
    {
        // runt methods uit interface
        Vector3 inputDirection = playerInput.GetMovementInput();
        movement.Move(inputDirection);
        if (playerInput.GetAttackInput())
        {
            if (attack != null)
            {
                attack.Attack();
            }
        }
        SwapAttack();
    }

    private void Initialise()
    {
        //Pakt eerste script dat interface gebruikt
        playerInput = GetComponent<IPlayerInput>();
        movement = GetComponent<IMovement>();
        attack = GetComponent<IAttack>();
    }

private void SwapAttack()
{
    gameObject.TryGetComponent<PlayerAttack>(out PlayerAttack playerAttack);
    gameObject.TryGetComponent<PlayerLightAttack>(out PlayerLightAttack lightAttack);

    if (Input.GetKeyDown(KeyCode.Q))
    {
        if (playerAttack != null)
        {
            DestroyImmediate(playerAttack);
            attack = gameObject.AddComponent<PlayerLightAttack>();
        }
        else if (lightAttack != null)
        {
            DestroyImmediate(lightAttack);
            attack = gameObject.AddComponent<PlayerAttack>();
        }
    }
}
}
