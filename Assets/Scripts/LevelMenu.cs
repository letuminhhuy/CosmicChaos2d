using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    private void Awake()
    {
   
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
        /*PlayerPrefs.SetInt("UnlockedLevel", 1);
        PlayerPrefs.SetInt("ReachedIndex", 1);
        PlayerPrefs.Save();*/
    }

    public void OpenLevel(string mapName)
    {
        SceneManager.LoadScene(mapName);
    }
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("UnlockedLevel", 1); // Đặt lại level mở khóa về 1
        PlayerPrefs.Save();

        // Cập nhật UI sau khi reset
        Awake();
    }
}
