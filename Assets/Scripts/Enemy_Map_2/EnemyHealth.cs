using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    private const float MAX_HEALTH = 100f;
    [SerializeField] private Transform healthBarFill;
    private Animator animator;
    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        health -= amount;
        health = Mathf.Max(health, 0);
        UpdateHealthBar();

        if (health <= 0)
        {
            Die();
        }
    }

    public void ResetHealth()
    {
        if (!isDead)
        {
            health = MAX_HEALTH;
            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        float healthPercent = health / MAX_HEALTH;
        healthBarFill.localScale = new Vector3(healthPercent, 1, 1);
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetBool("isDead", true);
        Destroy(gameObject, 2f);
    }

    public bool IsDead()
    {
        return isDead;
    }
}
