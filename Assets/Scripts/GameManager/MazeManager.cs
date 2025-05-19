using UnityEngine;

public class MazeManager : MonoBehaviour
{
    private int collectedCount = 0;

    private void enableExit()
    {
        GameObject.Find("Exit").GetComponent<Collider2D>().isTrigger = true;
    }

    public void unloadMaze()
    {
        // Unload the maze scene
        SceneLoader.Instance.CloseMaze();
    }

    public void addCollected()
    {
        collectedCount++;
        Debug.Log("Collected: " + collectedCount);
        if (collectedCount >= 4)
        {
            Debug.Log("All collectibles collected!");
            // Unload the maze scene
            enableExit();

        }
    }
}
