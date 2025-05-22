using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    private string mazeScene = "PCG-Maze"; // name of the maze scene
    private Scene main; // reference to the main scene

    void Awake()
    {
        //Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);   // survives additive loads
        }
        else
        {
            Destroy(gameObject);             // keep only one
        }//--- End of singleton pattern
    }

    public void OpenMaze(string mainScene)
    {
        // cache the main scene reference
        main = SceneManager.GetSceneByName(mainScene);

        // pause main scene
        foreach (var go in main.GetRootGameObjects())
            go.SetActive(false);

        // load maze scene additively & make it the active scene
        SceneManager.LoadSceneAsync(mazeScene, LoadSceneMode.Additive)
                    .completed += _ =>
                    {
                        SceneManager.SetActiveScene(SceneManager.GetSceneByName(mazeScene));
                    };
    }

    public void CloseMaze()
    {
        // unload maze scene
        SceneManager.UnloadSceneAsync(mazeScene);

        // resume main scene
        foreach (var go in main.GetRootGameObjects())
            go.SetActive(true);

        SceneManager.SetActiveScene(main);
    }

}
