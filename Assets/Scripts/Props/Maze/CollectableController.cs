using UnityEngine;

public class CollectableController : MonoBehaviour
{
    public MazeManager mazeManager;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Detected with: " + collision.gameObject.name);
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player collected the item!");
            // Find the MazeManager game object and its MazeManager instance
            mazeManager = GameObject.Find("MazeManager").GetComponent<MazeManager>();
            mazeManager.addCollected();
            Destroy(gameObject);
        }
    }
}