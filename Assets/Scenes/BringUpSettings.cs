using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BringUpSettings : MonoBehaviour
{
    [Header("Settings Panel")]
    public GameObject settingsPanel;

    [Header("Game State")]
    public bool isPaused = false;
    public bool isSettingsOpen = false;

    [Header("Key Bindings")]
    public KeyCode pauseKey = KeyCode.Escape;

    [Header("UI Elements")]
    public Slider sensitivitySlider;
    public Button goBackButton;
    public Button leaveButton;

    [Header("Scene Management")]
    public string mainMenuSceneName = "MainMenu";

    [Header("References")]
    public Movement playerMovement;

    private float previousTimeScale;

    void Start()
    {
        // Ensure settings are closed at start
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        previousTimeScale = Time.timeScale;

        // Set up the slider connection to the movement script
        if (sensitivitySlider != null && playerMovement != null)
        {
            sensitivitySlider.onValueChanged.AddListener(playerMovement.AdjustSpeed);

            // Load saved sensitivity value
            if (PlayerPrefs.HasKey("Sensitivity"))
            {
                float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity");
                sensitivitySlider.value = savedSensitivity;
            }
            else
            {
                sensitivitySlider.value = 1f;
            }
        }

        // Set up the back button click event
        if (goBackButton != null)
        {
            goBackButton.onClick.AddListener(ResumeGame);
            // Make sure the button is initially active (not disabled)
            goBackButton.gameObject.SetActive(true);
        }

        // Set up the leave button click event
        if (leaveButton != null)
        {
            leaveButton.onClick.AddListener(LeaveToMainMenu);
            leaveButton.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        // Check for pause input
        if (Input.GetKeyDown(pauseKey))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Store current time scale and pause the game
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;

            // Show the settings panel
            if (settingsPanel != null)
                settingsPanel.SetActive(true);

            // Ensure the back button is visible when settings are shown
            if (goBackButton != null)
                goBackButton.gameObject.SetActive(true);

            isSettingsOpen = true;

            // Enable cursor for settings interaction
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // Resume game
            Time.timeScale = previousTimeScale;

            // Hide settings panel
            if (settingsPanel != null)
                settingsPanel.SetActive(false);

            isSettingsOpen = false;

            // Re-lock cursor for gameplay
            Cursor.lockState = CursorLockMode.Locked;
            // Uncomment if needed: Cursor.visible = false;
        }
    }

    public void ResumeGame()
    {
        // Make sure to save sensitivity before resuming
        if (sensitivitySlider != null && playerMovement != null)
        {
            PlayerPrefs.SetFloat("Sensitivity", sensitivitySlider.value);
            PlayerPrefs.Save();
        }

        TogglePause();
    }

    public void LeaveToMainMenu()
    {
        // Save sensitivity before leaving
        if (sensitivitySlider != null)
        {
            PlayerPrefs.SetFloat("Sensitivity", sensitivitySlider.value);
            PlayerPrefs.Save();
        }

        // Reset time scale before loading a new scene
        Time.timeScale = 1.0f;

        // Unlock cursor for menu navigation
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Check if we're already in the main menu scene to avoid loading it twice
        if (SceneManager.GetActiveScene().name != mainMenuSceneName)
        {
            // Load the main menu scene with crossfade
            LevelManager.Instance.LoadScene(mainMenuSceneName, "CrossFade");
        }
        else
        {
            Debug.Log("Already in the main menu scene");
        }
    }
}
