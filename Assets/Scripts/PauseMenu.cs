using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject optionsPanel;

    [Header("Options Controls")]
    public Slider volumeSlider;
    public Toggle fullscreenToggle;

    private bool isPaused = false;
    private float currentVolume = 1f;

    void Start()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);

        currentVolume = AudioListener.volume;
        volumeSlider.value = currentVolume;
        fullscreenToggle.isOn = Screen.fullScreen;

        volumeSlider.onValueChanged.AddListener(SetVolume);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsPanel.activeSelf)
            {
                CloseOptions();
            }
            else
            {
                if (isPaused)
                    ResumeGame();
                else
                    PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        currentVolume = volume;
        AudioListener.volume = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
