
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    //finds game objects
    [Header("Menu Panels")]
    public GameObject mainMenuPanel;
    public GameObject howToPlayPanel;

    //opens the gameplay scene.
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    //opens the How To Play panel.
    public void OpenHowToPlay()
    {
        mainMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    //returns to the main menu.
    public void BackToMainMenu()
    {
        howToPlayPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    //quits the game.
    public void QuitGame()
    {
        Application.Quit();
    }
}
