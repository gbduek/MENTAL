using UnityEngine;

public class MemoryGame : MonoBehaviour
{
    public GameObject[] cards; // Array to hold the card GameObjects
    public GameObject cardPrefab; // Prefab for the card GameObject
    public int numberOfCards = 1; // Number of cards to create
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        InitializeGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Initialize the game by creating card instances
    void InitializeGame()
    {
        cards = new GameObject[1];
        cards[0] = Instantiate(cardPrefab, this.transform);

        cards[0].SetActive(true);
    }
}
