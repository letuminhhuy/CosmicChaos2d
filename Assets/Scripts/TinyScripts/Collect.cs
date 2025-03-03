using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Collect : MonoBehaviour
{
    private int count = 0;
    [SerializeField] private TextMeshProUGUI stoneText;
    [SerializeField] private Manager gameManager;

    void Start()
    {
        UpdateStone();
    }

    public void AddStone(int x)
    {
        count += x;
        UpdateStone();
        if (count >= 1)
        {
            gameManager.GameWin();
            UnlockNewLevel();
        }
    }
    public void UpdateStone()
    {
        stoneText.text = count.ToString() + "/7";
    }

    void UnlockNewLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
