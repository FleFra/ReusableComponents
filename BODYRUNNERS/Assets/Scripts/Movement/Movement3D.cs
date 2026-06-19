using UnityEngine;
public class Movement3D : MonoBehaviour, IMovement
{
    public float moveSpeed = 3f;
    private Rigidbody rb;

    private void Start()
    {
        // pakt rigidbody voor collision
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        // beweeg via rigidbody zodat collision werkt
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
    }
}