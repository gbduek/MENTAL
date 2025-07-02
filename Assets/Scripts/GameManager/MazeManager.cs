using UnityEngine;

public class MazeManager : MonoBehaviour
{
    [SerializeField]
    private MazeCell mazeCellPrefab;

    [SerializeField]
    private int mazeWidth;

    [SerializeField]
    private int mazeDepth;

    [SerializeField]
    private GameObject[] Collectables;

    [SerializeField]
    private MazeGenerator generator;
    private int collectedCount = 0;

    private MazeCell exit;

    void Start()
    {
        exit = generator.Generate(ref mazeCellPrefab, mazeWidth, mazeDepth, ref Collectables);
    }

    private void enableExit()
    {
        // Enable the exit
        Debug.Log("Enabling exit...");
        exit.enableExit();
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
        if (collectedCount == Collectables.Length)
        {
            Debug.Log("All collectibles collected!");
            // Unload the maze scene
            enableExit();
        }
    }
}
