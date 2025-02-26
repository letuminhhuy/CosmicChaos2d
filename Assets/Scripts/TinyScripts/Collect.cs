using UnityEngine;
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


    void Update()
    {

    }

    public void AddStone(int x)
    {
        count += x;
        UpdateStone();
        if (count >= 1)
        {
            gameManager.GameWinMenu();
        }
    }
    public void UpdateStone()
    {
        stoneText.text = count.ToString() + "/7";
    }
}
