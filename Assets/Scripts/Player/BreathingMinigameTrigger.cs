using UnityEngine;

public class BreathingMinigameTrigger : MonoBehaviour
{
    public BreathingMinigame minigame;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            minigame.OpenMinigame(); 
            gameObject.SetActive(false); 
        }
    }
}
