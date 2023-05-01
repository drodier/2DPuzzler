using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public Button resumeButton;
    public Button settingsButton;
    public Button mainMenuButton;
    public Button quitButton;
    public Button backButton;

    void Start()
    {
        // Get reference to the resume button component and add an event listener to call the ResumeGame method when clicked
        resumeButton = pauseMenu.transform.Find("ResumeButton").GetComponent<Button>();
        resumeButton.onClick.AddListener(ResumeGame);

        // Get reference to the settings button component and add an event listener to call the OpenSettings method when clicked
        settingsButton = pauseMenu.transform.Find("SettingsButton").GetComponent<Button>();
        settingsButton.onClick.AddListener(OpenSettings);

        // Get reference to the main menu button component and add an event listener to call the LoadMainMenu method when clicked
        mainMenuButton = pauseMenu.transform.Find("MainMenuButton").GetComponent<Button>();
        mainMenuButton.onClick.AddListener(LoadMainMenu);

        // Get reference to the quit button component and add an event listener to call the QuitGame method when clicked
        quitButton = pauseMenu.transform.Find("QuitButton").GetComponent<Button>();
        quitButton.onClick.AddListener(QuitGame);

        // Get reference to the quit button component and add an event listener to call the QuitGame method when clicked
        backButton = settingsMenu.transform.Find("BackButton").GetComponent<Button>();
        backButton.onClick.AddListener(BackToPauseMenu);

        // Hide the pause menu on start
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    void Update()
    {
        // Check if the escape key is pressed and toggle pause state accordingly
        if (Input.GetButtonDown("Pause"))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        // Pause the game and show the pause menu
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame()
    {
        // Resume the game and hide the pause menu
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void OpenSettings()
    {
        // Disable the PauseMenu and activate the SettingsMenu
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void BackToPauseMenu()
    {
        // Disable the SettingsMenu and activate the PauseMenu
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }
}
