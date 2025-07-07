using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;

    [Header("Options Controls")]
    public Slider volumeSlider;
    public Toggle fullscreenToggle;

    private float currentVolume = 1f;

    void Start()
    {
        // Inicializa os controles com os valores atuais
        AudioManager.instance.PlaySound("MenuMusic");
        volumeSlider.value = currentVolume;
        fullscreenToggle.isOn = Screen.fullScreen;

        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // Fun��es dos bot�es do menu principal
    public void StartNewGame()
    {
        SceneManager.LoadScene("Menu_Chapters");
    }

    public void LoadLucas()
    {
        SceneManager.LoadScene("");
    }

    public void LoadSindra()
    {
        SceneManager.LoadScene("Sindra-Room");
    }

    public void LoadGame()
    {
        Debug.Log("Load Game clicked");
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // Fun��es dos controles do painel op��es
    public void SetVolume(float volume)
    {
        currentVolume = volume;
        AudioListener.volume = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void QuitGame()
    {
        Debug.Log("Quit clicked");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
