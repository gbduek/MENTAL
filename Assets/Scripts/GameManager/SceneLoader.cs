using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    private string secondScene;
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

    public void SetMainScene(string mainScene)
    {
        // cache the main scene reference
        main = SceneManager.GetSceneByName(mainScene); ;
    }

    public void OpenSecondScene(string sndScene)
    {
        
        secondScene = sndScene;
        // pause main scene
        foreach (var go in main.GetRootGameObjects())
            go.SetActive(false);

        // load maze scene additively & make it the active scene
        SceneManager.LoadSceneAsync(secondScene, LoadSceneMode.Additive)
                    .completed += _ =>
                    {
                        SceneManager.SetActiveScene(SceneManager.GetSceneByName(secondScene));
                    };
    }

    public void CloseSecondScene()
    {
        // unload maze scene
        SceneManager.UnloadSceneAsync(secondScene);

        // resume main scene
        foreach (var go in main.GetRootGameObjects())
            go.SetActive(true);

        SceneManager.SetActiveScene(main);
    }
}
