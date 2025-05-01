using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public MovementProfile profile;
    public Transform forcedTarget;

    private Vector2 movementInput;

    void Start()
    {
        forcedTarget = GameObject.FindWithTag("AttractedTo").transform;
    }

    void Update()
    {
        // Apply movement every frame
        Vector3 movement = new Vector3(movementInput.x, movementInput.y, 0f);
        transform.position += movement * profile.speed * Time.deltaTime;
        // If forced movement is enabled, move towards the target
        if (profile.isForcedMovement && forcedTarget != null)
        {
            Vector3 targetPosition = forcedTarget.position;
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * profile.forcedSpeed * Time.deltaTime;

        }


    }

    // This function is automatically called by the new Input System
    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }
}
