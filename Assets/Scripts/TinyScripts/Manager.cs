using UnityEngine;

public class Manager : MonoBehaviour
{

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseGameMenu;

    void Start()
    {
        MainMenu();
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        pauseGameMenu.SetActive(false);

        Time.timeScale = 0f;
    }
    public void GameOverMenu()
    {
        gameOverMenu.SetActive(true);
        mainMenu.SetActive(false);
        pauseGameMenu.SetActive(false);

        Time.timeScale = 0f;
    }
    public void PauseGameMenu()
    {
        pauseGameMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        mainMenu.SetActive(false);

        Time.timeScale = 0f;
    }
    public void StartGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseGameMenu.SetActive(false);

        Time.timeScale = 1f;
    }
    public void ResumeGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseGameMenu.SetActive(false);

        Time.timeScale = 1f;
    }
}
