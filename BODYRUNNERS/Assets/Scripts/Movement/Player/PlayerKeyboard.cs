using UnityEngine;

public class PlayerKeyboard : MonoBehaviour, IPlayerInput
{
    //pakt de keyboard input van de speler
    public Vector3 GetMovementInput() 
    {
        Vector3 currentInput = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) currentInput.z++;
        if (Input.GetKey(KeyCode.S)) currentInput.z--;
        if (Input.GetKey(KeyCode.D)) currentInput.x++;
        if (Input.GetKey(KeyCode.A)) currentInput.x--;

        // rotate de speler in de juiste richting
        if (currentInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(currentInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // returned normalized input
        return currentInput.normalized;
    }

    // Speler attack input
    public bool GetAttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }
        return false;
    }
}
