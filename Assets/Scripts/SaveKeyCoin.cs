using TMPro;
using UnityEngine;

public class SaveKeyCoin : MonoBehaviour
{
    private int keyCount;
    private int coinCount;

    [SerializeField] private TextMeshProUGUI keyText;
    [SerializeField] private TextMeshProUGUI coinText;

    void Start()
    {
        LoadProgress();
        UpdateUI();
    }

    public void AddKey(int amount)
    {
        keyCount += amount;
        PlayerPrefs.SetInt("KeyCount", keyCount);
        PlayerPrefs.Save();
        UpdateUI();
    }

    public void AddCoin(int amount)
    {
        coinCount += amount;
        PlayerPrefs.SetInt("CoinCount", coinCount);
        PlayerPrefs.Save();
        UpdateUI();
    }

    public int GetKeyCount()
    {
        return PlayerPrefs.GetInt("KeyCount", 0);
    }

    public int GetCoinCount()
    {
        return PlayerPrefs.GetInt("CoinCount", 0);
    }

    private void LoadProgress()
    {
        keyCount = PlayerPrefs.GetInt("KeyCount", 0);
        coinCount = PlayerPrefs.GetInt("CoinCount", 0);
    }

    private void UpdateUI()
    {
        keyText.text = "Key: " + keyCount;
        coinText.text = "Coin: " + coinCount;
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("KeyCount");
        PlayerPrefs.DeleteKey("CoinCount");
        PlayerPrefs.Save();
        LoadProgress();
        UpdateUI();
    }
}
