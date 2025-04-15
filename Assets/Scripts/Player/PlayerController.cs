using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    private Vector2 movementInput;

    void Update()
    {
        // Apply movement every frame
        Vector3 movement = new Vector3(movementInput.x, movementInput.y, 0f);
        transform.position += movement * speed * Time.deltaTime;
    }

    // This function is automatically called by the new Input System
    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }
}
