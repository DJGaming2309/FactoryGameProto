using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{
    //looks for panels
    [Header("Menu Panels")]
    public GameObject pauseMenuPanel;
    public GameObject howToPlayPanel;

    //finds the grid placement preview
    [Header("References")]
    public GridPlacementPreview gridPlacementPreview;

    private bool isPaused = false;

    void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            //gives conveyor placement priority.
            if (!isPaused && gridPlacementPreview != null &&
                gridPlacementPreview.IsPlacementModeActive)
            {
                gridPlacementPreview.CancelPlacementMode();
                return;
            }

            if (isPaused)
            {
                if (howToPlayPanel.activeSelf)
                {
                    BackToPauseMenu();
                }
                else
                {
                    ResumeGame();
                }
            }
            else
            {
                PauseGame();
            }
        }
    }

    //button pressed pauses game
    public void PauseGame()
    {
        isPaused = true;
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    //button pressed resumes game
    public void ResumeGame()
    {
        isPaused = false;

        pauseMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    //button pressed opens how to play screen
    public void OpenHowToPlay()
    {
        pauseMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    //button pressed goes back to pause menu
    public void BackToPauseMenu()
    {
        howToPlayPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    //button pressed goes back to main menu
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}