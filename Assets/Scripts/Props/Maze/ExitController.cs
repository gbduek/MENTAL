using UnityEngine;

public class ExitController : MonoBehaviour
{
    public MazeManager mazeManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached the exit!");
            mazeManager = GameObject.Find("MazeManager").GetComponent<MazeManager>();
            mazeManager.unloadMaze();
        }
    }
}

