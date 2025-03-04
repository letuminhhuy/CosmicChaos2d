using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

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
            StartCoroutine(DelayGameWin());
            //gameManager.GameWin();
            UnlockNewLevel();
        }
    }
    private IEnumerator DelayGameWin()
    {
        yield return new WaitForSeconds(1);
        gameManager.GameWin();
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
