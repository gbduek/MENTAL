using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementInput;
    private CharacterMetrics characterMetrics;
    private Animator animator;

    public MovementProfile profile;
    public Transform forcedTarget;

    private bool isInputLocked = false;
    private float inputLockTimer = 0f;

    private float forcedMovementFluctuation = 0f;
    private float fluctuationTimer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterMetrics = GetComponent<CharacterMetrics>();
        //Find the target object with the tag "AttractedTo" and set it as the forced target
        GameObject targetObject = GameObject.FindWithTag("AttractedTo");
        if (targetObject != null)
        {
            forcedTarget = targetObject.transform;
        }
    }

    void Update()
    {
        HandleInputLock();

        if (!isInputLocked)
        {
            // Calculate speed decay based on anxiety
            // When anxiety = 0, speed = profile.speed
            // When anxiety = 100, speed = profile.forcedSpeed
            float anxiety = characterMetrics != null ? characterMetrics.getAnxiety() : 0f;
            float t = Mathf.Clamp01(anxiety / 100f);
            float currentSpeed = Mathf.Lerp(profile.speed, profile.forcedSpeed*profile.maxForcedMultiplier, t);
            Debug.Log(currentSpeed);
            Vector3 inputMovement = new Vector3(movementInput.x, movementInput.y, 0f);
            transform.position += inputMovement * currentSpeed * Time.deltaTime;

            if (Random.value < profile.inputLockChance * Time.deltaTime)
            {
                LockInput(profile.inputLockDuration);
            }
        }

        ApplyForcedMovement();
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

    private void HandleInputLock()
    {
        if (isInputLocked)
        {
            inputLockTimer -= Time.deltaTime;
            if (inputLockTimer <= 0f)
            {
                isInputLocked = false;
            }
        }
    }

    private void LockInput(float duration)
    {
        isInputLocked = true;
        inputLockTimer = duration;
        // Visual feedback ?
    }

    private void ApplyForcedMovement()
    {
        if (profile.isForcedMovement && forcedTarget != null)
        {
            Vector3 direction = (forcedTarget.position - transform.position).normalized;

            // Fluctuating force: changes every fluctuationInterval
            fluctuationTimer -= Time.deltaTime;
            if (fluctuationTimer <= 0f)
            {
                forcedMovementFluctuation = Random.Range(profile.minForcedMultiplier, profile.maxForcedMultiplier);
                fluctuationTimer = profile.fluctuationInterval;
            }

            float appliedForce = profile.forcedSpeed * forcedMovementFluctuation;

            transform.position += direction * appliedForce * Time.deltaTime;

            // Occasional jerks
            if (Random.value < profile.jerkChance * Time.deltaTime)
            {
                transform.position += direction * profile.jerkStrength;
                // visual feedback ?
            }
        }
    }
}
