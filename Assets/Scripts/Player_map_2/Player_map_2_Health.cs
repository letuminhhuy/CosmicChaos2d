using System.Collections;
using UnityEngine;

public class Player_map_2_Health : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    private const float MAX_HEALTH = 100f;
    private GameManager gameManager;
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

        if (health <= 0)
        {
            health = 0;
            StartCoroutine(GameOverAfterDelay(2f)); // Đợi 2 giây trước khi hiển thị Game Over
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
    }

    public float GetHealth()
    {
        return health;
    }

}