using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject pauseGame;
    [SerializeField] private GameObject gameWin;
    [SerializeField] private GameObject pauseButton;

    void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        pauseGame.SetActive(false);
        gameOver.SetActive(false);
        gameWin.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }
    public void PauseGame()
    {
        pauseGame.SetActive(true);
        gameOver.SetActive(false);
        gameWin.SetActive(false);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        gameOver.SetActive(false);
        pauseGame.SetActive(false);
        gameWin.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        pauseGame.SetActive(false);
        gameWin.SetActive(false);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void GameWin()
    {
        gameWin.SetActive(true);
        gameOver.SetActive(false);
        pauseGame.SetActive(false);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }
}
