using UnityEngine;

public class MemoryGame : MonoBehaviour
{
    public GameObject canvasChild;
    public GameObject[] cards; // Array to hold the card GameObjects
    public GameObject cardPrefab; // Prefab for the card GameObject
    public int numberOfCards = 1; // Number of cards to create
    private bool gameStarted = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.name);
        if (!gameStarted)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player entered the trigger area");
                // Start the game when the player enters the trigger
                InitializeGame();
                gameStarted = true;
            }
        }
    }

    // Initialize the game by creating card instances
    void InitializeGame()
    {
        Time.timeScale = 0;
        canvasChild.SetActive(true);
        cards = new GameObject[3];

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i] = Instantiate(cardPrefab, canvasChild.transform);
            CardController cardController = cards[i].GetComponent<CardController>();
            cardController.SetupCard("Card" + (i + 1).ToString());
            cards[i].SetActive(true);
        }
    }
}
