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
    public void ContinueGame()
    {
        gameManager.ResumeGame();
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextPlanet()
    {
        SceneManager.LoadScene("Sence_2");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    //method to choose map from menu
    public void SelectMapTinyMap()
    {
        SceneManager.LoadScene("TinyMap");
    }

    public void SelectMapScene2()
    {
        SceneManager.LoadScene("Scene_2");
    }

}
