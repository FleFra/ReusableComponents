using UnityEngine;

public interface IPlayerInput
{
    Vector3 GetMovementInput();

    bool GetAttackInput();
}
