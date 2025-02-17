using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHP = 100f;
    protected float currentHP;
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        animator.SetBool("isDead", true);
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.5f);
    }
}
