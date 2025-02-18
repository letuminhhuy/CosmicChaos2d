using UnityEngine;
using TMPro;

public class Collect : MonoBehaviour
{
    private int count = 0;
    [SerializeField] private TextMeshProUGUI stoneText;

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
    }
    public void UpdateStone()
    {
        stoneText.text = count.ToString() + "/7";
    }
}
