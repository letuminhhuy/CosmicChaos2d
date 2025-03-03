using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
   public void OpenLevel(string mapName)
    {
        SceneManager.LoadScene(mapName);
    }
}
