using System.Collections;
using UnityEngine;

public class Player_map_2_Health : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    private const float MAX_HEALTH = 100f;
    private GameManager gameManager;
    [SerializeField] private Transform healthBarFill;
    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {

        // Testing damage and healing
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Damage(10);

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Heal(10);
        }


    }


    public void Damage(float amount)
    {
        if (amount < 0) return;

        health -= amount;
        UpdateHealthBar();

        if (health <= 0)
        {
            health = 0;
            StartCoroutine(GameOverAfterDelay(0.5f)); // Đợi 1 giây trước khi hiển thị Game Over
        }
    }

    private IEnumerator GameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Đợi thời gian được chỉ định
        gameManager.GameOver(); // Hiển thị Game Over UI
    }


    public void Heal(float amount)
    {
        if (amount < 0) return;

        health = Mathf.Min(health + amount, MAX_HEALTH);
        UpdateHealthBar(); // Cập nhật thanh máu
    }

    public float GetHealth()
    {
        return health;
    }
    void UpdateHealthBar()
    {
        float healthPercent = health / MAX_HEALTH;
        healthBarFill.transform.localScale = new Vector3(healthPercent, 1, 1);
    }
}