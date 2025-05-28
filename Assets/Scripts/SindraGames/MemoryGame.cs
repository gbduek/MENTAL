using UnityEngine;

public class MemoryGame : MonoBehaviour
{
    public Transform canvasChild;
    public GameObject[] cards; // Array to hold the card GameObjects
    public GameObject cardPrefab; // Prefab for the card GameObject
    public int numberOfCards = 1; // Number of cards to create

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.name);
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger area");
            // Start the game when the player enters the trigger
            InitializeGame();
        }
    }

    // Initialize the game by creating card instances
    void InitializeGame()
    {
        canvasChild.gameObject.GetComponent<Canvas>().enabled = true;
        cards = new GameObject[1];
        cards[0] = Instantiate(cardPrefab, canvasChild);

        cards[0].SetActive(true);
    }
}
