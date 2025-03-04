using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tiny_Player : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float attackRange = 1.6f;
    public int attackDamage = 10;
    private float lastAttackTime = 0f;
    [SerializeField] private float attackCooldown = 0.5f;
    public Transform attackPoint;
    public LayerMask enemyLayer;

    private float currentHp;
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private Image hpBar;

    private bool isDead = false;

    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer rbSprite;
    private Animator animator;
    
    private Collect collect;
    [SerializeField] private Manager gameManager;
    [SerializeField] private AudioManager audioManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collect = FindAnyObjectByType<Collect>();
    }

    void Start()
    {
        currentHp = maxHp;
        UpdateHpBar();
    }

    void Update()
    {
        HandleMove();

        if (Input.GetMouseButtonDown(0) && !IsMoving() && Time.timeScale != 0f)
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                rb.linearVelocity = Vector2.zero;
                animator.SetTrigger("isAttack");
                HandleAttack();
                audioManager.HitSound();

                lastAttackTime = Time.time;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.PauseGame();
        }

        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    currentHp = 0;
        //    TakeDamage(currentHp);
        //}
    }

    void HandleMove()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = movement * moveSpeed;
        animator.SetBool("isRun", movement != Vector2.zero);

        if (movement.x > 0)
        {
            rbSprite.flipX = false;
        }
        else if (movement.x < 0)
        {
            rbSprite.flipX = true;
        }
    }

    void HandleAttack()
    {
        // Get all colliders within the attack range of the attackPoint
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    //Display the attack range of the attack point
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(float damage)
    {
        if (Time.timeScale == 0f || isDead) return;

        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        UpdateHpBar();
        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHp / maxHp;
        }
    }

    private void Die()
    {
        isDead = true;
        audioManager.DeathSound();
        animator.SetTrigger("isDead");
        rb.linearVelocity = Vector2.zero; rb.simulated = false;
        GetComponent<Collider2D>().enabled = false;

        Invoke(nameof(ShowGameOverMenu), 1.4f);
    }

    private void ShowGameOverMenu()
    {
        gameManager.GameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnergyStone"))
        {
            collect.AddStone(1);
            audioManager.CollectSound();
            Destroy(collision.gameObject);
        }
    }

    public void Heal(float healAmount)
    {
        currentHp += healAmount;
        currentHp = Mathf.Min(currentHp, maxHp);
        UpdateHpBar();
        Debug.Log("Player healed: " + healAmount);
    }

    private bool IsMoving()
    {
        return movement != Vector2.zero;
    }
}
