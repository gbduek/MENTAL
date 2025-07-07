using UnityEngine;

public class ColliderQTE : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collision Detected with: " + collision.gameObject.name);
        }
    }
}

