using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 8;
    private float movementX;
    private float movementY;
    private int count;
    public int lives = 5;
    public KarmaController playerKarma;
    private Animator animator;
    private Vector2 lastMovementDir;
    private Vector2 movementInput;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        // Initialization code if needed
        // playerKarma.getKarma();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();

        if (movementInput == Vector2.zero)
        {
            animator.speed = 0f; // Freeze animation
            animator.SetFloat("MoveX", lastMovementDir.x);
            animator.SetFloat("MoveY", lastMovementDir.y);
        }
        else
        {
            animator.speed = 1f; // Resume animation
            animator.SetFloat("MoveX", movementInput.x);
            animator.SetFloat("MoveY", movementInput.y);
            animator.SetBool("IsMoving", movementInput != Vector2.zero);
            spriteRenderer.flipX = movementInput.x > 0;
            lastMovementDir = movementInput;
        }

        //playerKarma.setKarma(0.1f);
    }

    private void FixedUpdate()
    {
        Vector2 movement = movementInput.normalized;
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
    }
}
