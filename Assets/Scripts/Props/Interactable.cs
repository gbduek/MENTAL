using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public enum InteractionType { OnKeyPress, OnTriggerEnter }
    public InteractionType interactionType = InteractionType.OnKeyPress;
    public float interactCooldown = 0f;

    public UnityEvent onInteract;

    private bool playerInside;
    private float cooldownTimer = -1f;

    void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        if (interactionType == InteractionType.OnKeyPress && playerInside && Input.GetKeyDown(KeyCode.E))
        {
            if (cooldownTimer <= 0f)
            {
                cooldownTimer = interactCooldown;
                onInteract.Invoke();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = true;

        if (interactionType == InteractionType.OnTriggerEnter)
        {
            if (cooldownTimer <= 0f)
            {
                cooldownTimer = interactCooldown;
                onInteract.Invoke();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;
    }
}
