using UnityEngine;
using TMPro;

public class BossTriggerZone : MonoBehaviour
{
    public Boss boss; // Tham chiếu đến Boss
    public TextMeshProUGUI bossWarningText; // Tham chiếu đến UI Text thông báo

    private void Start()
    {
        if (bossWarningText != null)
        {
            bossWarningText.gameObject.SetActive(false); // Ẩn thông báo khi bắt đầu
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && boss != null)
        {
            boss.Appear(); // Gọi hàm để hiển thị Boss

            if (bossWarningText != null)
            {
                bossWarningText.gameObject.SetActive(true); // Hiển thị thông báo
                Invoke("HideWarning", 2f); // Ẩn thông báo sau 2 giây
            }
        }
    }

    private void HideWarning()
    {
        if (bossWarningText != null)
        {
            bossWarningText.gameObject.SetActive(false);
        }
    }
}
