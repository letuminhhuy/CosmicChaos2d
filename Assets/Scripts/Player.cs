using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer rbSprite;
    private Animator animator;

    [SerializeField]
    private float maxHp = 100f;
    private float currentHp;
    [SerializeField]
    private Image hpBar;

    public Transform attackPoint;  
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    public float attackDamage = 25f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        currentHp = maxHp;
        UpdateHpBar();
    }

    void Update()
    {
        MovePlayer();
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
            Attack();   
        }
    }
    void MovePlayer()
    {
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.linearVelocity = playerInput.normalized * moveSpeed;
        if (playerInput.x > 0)
        {
            rbSprite.flipX = false;
        }
        else if (playerInput.x < 0)
        {
            rbSprite.flipX = true;
        }

        if (playerInput != Vector2.zero)
        {
            animator.SetBool("isWalk", true);
        }
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Player1Attack"))
        {
            animator.SetBool("isWalk", false);
        }

    }
    public virtual void TakeDamege(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        UpdateHpBar();
        if (currentHp <= 0)
        {
            Die();
        }

    }
    private void Die()
    {
        animator.SetBool("isDead", true);
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;
        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject ,0.5f);
    }
    protected void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHp / maxHp; // thanh hp

        }
    }

    void Attack()
    {
        animator.SetTrigger("attack");

        // Kiểm tra va chạm với Enemy
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Debug.Log("Hit enemies count: " + hitEnemies.Length); 
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Attacking enemy: " + enemy.name); 
            enemy.GetComponent<EnemyAI>().TakeDamage(attackDamage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}
