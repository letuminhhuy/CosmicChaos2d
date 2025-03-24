using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    private float maxHealth;

    [SerializeField] private Transform healthBarFill;
    private Animator animator;
    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // Kiểm tra tag để thiết lập máu cho Enemy hoặc Boss
        if (CompareTag("Boss"))
        {
            maxHealth = 200f; // Thiết lập máu tối đa cho Boss
            health = maxHealth;
        }
        else if (CompareTag("Enemy"))
        {
            maxHealth = 100f; // Thiết lập máu tối đa cho Enemy
            health = maxHealth;
        }
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
            health = maxHealth;
            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        float healthPercent = health / maxHealth;
        healthBarFill.localScale = new Vector3(healthPercent, 1, 1);
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetBool("isDead", true);
        Destroy(gameObject, 2f);
    }
    // cho enemy 
    public bool IsDead()
    {
        return isDead;
    }
}