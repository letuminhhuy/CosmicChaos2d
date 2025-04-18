﻿using TMPro;
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
    [SerializeField] private GameObject pauseGame;
    [SerializeField] private GameObject pauseButton;

    private bool isGameOver = false;
    private bool isGameWin = false;

    [SerializeField] private AudioClip gameWinSound; // Âm thanh thắng
    [SerializeField] private AudioClip gameOverSound; // Âm thanh thua
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScore();
        UpdateKey();
        pauseButton.SetActive(true); // Bật Pause Button khi game chạy
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);
        Time.timeScale = 1f;
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
        if (isGameOver || isGameWin) return; // Nếu game over hoặc win, không làm gì cả

        key += keys; // Cộng thêm số chìa khóa
        if (key > totalKeys) key = totalKeys; // Giới hạn số chìa khóa tối đa

        UpdateKey(); // Cập nhật UI

        if (key >= totalKeys) // Kiểm tra nếu đã đủ chìa khóa
        {
            GameWin(); // Gọi GameWin
        }
    }



    /*public void GameOver()
    {
        isGameOver = true;
        score = 0;
        key = 0;
        Time.timeScale = 0;
        gameOverUI.SetActive(true); //hien thi panel gameover    
    }*/
    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        score = 0;
        key = 0;
        Time.timeScale = 0;
        // Xử lý Player, Enemy, và Boss
        string[] tags = { "Player", "Enemy", "Boss" };
        foreach (string tag in tags)
        {
            GameObject[] objects = (tag == "Enemy") ? GameObject.FindGameObjectsWithTag(tag) : new[] { GameObject.FindWithTag(tag) };
            foreach (GameObject obj in objects)
            {
                if (obj != null)
                {
                    Transform healthBar = obj.transform.Find("HealthBar");
                    if (healthBar != null) healthBar.gameObject.SetActive(false);
                }
            }
        }
        pauseButton.SetActive(false);
        // Hiển thị UI Game Over
        if (gameOverUI != null) gameOverUI.SetActive(true);

        // Phát âm thanh Game Over
        PlaySound(gameOverSound);
    }

    public void GameWin()
    {
        isGameWin = true;
        score = 0;
        key = 0;
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        gameWinUI.SetActive(true);
        // Xử lý Player, Enemy, và Boss
        string[] tags = { "Player", "Enemy", "Boss" };
        foreach (string tag in tags)
        {
            GameObject[] objects = (tag == "Enemy") ? GameObject.FindGameObjectsWithTag(tag) : new[] { GameObject.FindWithTag(tag) };
            foreach (GameObject obj in objects)
            {
                if (obj != null)
                {
                    Transform healthBar = obj.transform.Find("HealthBar");
                    if (healthBar != null) healthBar.gameObject.SetActive(false);
                }
            }
        }

        // Phát âm thanh Game Win
        PlaySound(gameWinSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    public void RestartGame()
    {
        isGameOver=false;
        score = 0;
        key = 0;
        Time.timeScale = 1;
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
        SceneManager.LoadScene("MainMenuScenes");
        Time.timeScale = 1;
    }
    public void PauseGame()
    {
        pauseGame.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
        // Xử lý Player, Enemy, và Boss
        string[] tags = { "Player", "Enemy", "Boss" };
        foreach (string tag in tags)
        {
            GameObject[] objects = (tag == "Enemy") ? GameObject.FindGameObjectsWithTag(tag) : new[] { GameObject.FindWithTag(tag) };
            foreach (GameObject obj in objects)
            {
                if (obj != null)
                {
                    Transform healthBar = obj.transform.Find("HealthBar");
                    if (healthBar != null) healthBar.gameObject.SetActive(false);
                }
            }
        }
    }
    public void ResumeGame()
    {
        pauseGame.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }
}
