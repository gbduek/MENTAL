using UnityEngine;

public class CardController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        // Handle card click event
        Debug.Log("Card clicked: " + gameObject.name);
        // Add your card flip logic here
    }

    void OnMouseUp()
    {
        // Handle card release event
        Debug.Log("Card released: " + gameObject.name);
        // Add your card flip logic here
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
