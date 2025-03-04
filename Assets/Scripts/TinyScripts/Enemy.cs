using UnityEngine;
using UnityEngine.UI; 

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float maxHP = 100f;
    protected float currentHP;

    //[SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected float damage = 1f;

    [SerializeField] protected Image hpBar;

    protected Rigidbody2D rb;
    protected Animator animator;
    protected bool isFacingRight = true;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHP = maxHP;
        UpdateHPBar();
    }

    public virtual void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);
        UpdateHPBar();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected void UpdateHPBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHP / maxHP;
        }
    }

    protected void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}