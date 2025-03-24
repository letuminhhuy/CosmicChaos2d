using UnityEngine;

public class SavePosition : MonoBehaviour
{
    void Start()
    {
        LoadPlayerPosition();
    }

    void Update()
    {
        SavePlayerPositon();
    }
    public void SavePlayerPositon()
    {
        Vector3 playerPosition = transform.position;
        PlayerPrefs.SetFloat("x", transform.position.x);
        PlayerPrefs.SetFloat("y", transform.position.y);
        PlayerPrefs.SetFloat("z", transform.position.z);
    }

    public void LoadPlayerPosition()
    {
        if (PlayerPrefs.HasKey("x") && PlayerPrefs.HasKey("y") && PlayerPrefs.HasKey("z"))
        {
            float x = PlayerPrefs.GetFloat("x");
            float y = PlayerPrefs.GetFloat("y");
            float z = PlayerPrefs.GetFloat("z");
            transform.position = new Vector3(x, y, z);
        }
        
    }
}
