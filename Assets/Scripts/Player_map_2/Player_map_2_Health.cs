using UnityEngine;

public class Player_map_2_Health : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    private const float MAX_HEALTH = 100f;

    private void Awake()
    {
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