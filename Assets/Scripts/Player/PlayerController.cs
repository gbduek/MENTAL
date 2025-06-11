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

    void Start()
    {
        // Initialization code if needed
        // playerKarma.getKarma();
        animator = GetComponent<Animator>();
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();

        if (movementInput == Vector2.zero)
        {
            animator.speed = 0f; // Freeze animation
            animator.Play("sindra_walk", 0, 0f); // Show first frame of walk animation
        }
        else
        {
            animator.speed = 1f; // Resume animation
        }

        animator.SetFloat("MoveX", movementInput.x);
        animator.SetFloat("MoveY", movementInput.y);
        animator.SetBool("IsMoving", movementInput != Vector2.zero);

        //playerKarma.setKarma(0.1f);
    }

    private void Update()
    {
        Vector2 movement = movementInput.normalized;
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
