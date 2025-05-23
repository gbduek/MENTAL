using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public MovementProfile profile;
    public Transform forcedTarget;

    private Vector2 movementInput;
    private CharacterMetrics characterMetrics;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        //Find the target object with the tag "AttractedTo" and set it as the forced target
        GameObject targetObject = GameObject.FindWithTag("AttractedTo");
        if (targetObject != null)
        {
            forcedTarget = targetObject.transform;
        }
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
        // Update movementInput first
        movementInput = value.Get<Vector2>();

        if (animator != null)
        {
            // Check if the player is moving
            if (movementInput == Vector2.zero)
            {
                //if not -> idle
                animator.SetBool("isWalking", false);
                animator.SetFloat("LastInputX", animator.GetFloat("InputX"));
                animator.SetFloat("LastInputY", animator.GetFloat("InputY"));
            }
            else
            {
                //if moving -> walking
                animator.SetBool("isWalking", true);
                animator.SetFloat("InputX", movementInput.x);
                animator.SetFloat("InputY", movementInput.y);
            }
        }
    }

}
