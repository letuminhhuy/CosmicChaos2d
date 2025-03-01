using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    //[SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseGameMenu;
    [SerializeField] private GameObject gameWinMenu;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenuScene")
        {
            MainMenu();
        }
        else
        {
            StartGame();
        }
    }

    public void MainMenu()
    {
        gameOverMenu.SetActive(false);
        pauseGameMenu.SetActive(false);
        gameWinMenu.SetActive(false);

        Time.timeScale = 0f;
    }

    public void GameOverMenu()
    {
        gameOverMenu.SetActive(true);
        pauseGameMenu.SetActive(false);
        gameWinMenu.SetActive(false);

        Time.timeScale = 0f;
    }

    public void PauseGameMenu()
    {
        pauseGameMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        gameWinMenu.SetActive(false);

        Time.timeScale = 0f;
    }

    public void GameWinMenu()
    {
        gameWinMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        pauseGameMenu.SetActive(false);

        Time.timeScale = 0f;
    }


    public void StartGame()
    {
        gameOverMenu.SetActive(false);
        pauseGameMenu.SetActive(false);
        gameWinMenu.SetActive(false);

        Time.timeScale = 1f;
    }

    public void ResumeGame()
    {
        gameOverMenu.SetActive(false);
        pauseGameMenu.SetActive(false);
        gameWinMenu.SetActive(false);

        Time.timeScale = 1f;
    }
}
