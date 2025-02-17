using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float score = 0;
    private int totalKeys = 3;
    private int key = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI keyText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameWinUI;

    private bool isGameOver = false;
    private bool isGameWin = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScore();
        UpdateKey();
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }
    public void AddScore(float points)
    {
        if (!isGameOver && !isGameWin)
        {
            score += points;
            UpdateScore();
        }

    }
    public void MinusScore(float points)
    {
        if (!isGameOver && !isGameWin)
        {
            score -= points;
            UpdateScore();
        }

    }
    private void UpdateKey()
    {
        keyText.text = $"{key}/{totalKeys}";
    }
    public void AddKey(int keys)
    {
        if (!isGameOver && !isGameWin)  
        {
            key += keys;
            if (key > totalKeys) key = totalKeys;  
            UpdateKey();

            if (key == totalKeys)  
            {
                GameWin(); 
            }
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        score = 0;
        key = 0;
        Time.timeScale = 0;
        gameOverUI.SetActive(true); //hien thi panel gameover    
    }

    public void GameWin()
    {
        isGameWin = true;
        score = 0;
        key = 0;
        Time.timeScale = 0;
        gameWinUI.SetActive(true);
    }
    public void RestartGame()
    {
        isGameOver=false;
        score = 0;
        key = 0;
        UpdateScore();
        UpdateKey();
        SceneManager.LoadScene("Sence_2");
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public bool IsGameWin()
    {
        return isGameWin;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu_Sence");
        Time.timeScale = 1;
    }
}
