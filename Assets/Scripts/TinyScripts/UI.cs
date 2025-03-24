using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private Manager gameManager;

    public void StartGame()
    {
        gameManager.StartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        gameManager.ResumeGame();
    }

    public void PauseGame()
    {
        gameManager.PauseGame();
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Sence_2");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScenes");
    }
}
