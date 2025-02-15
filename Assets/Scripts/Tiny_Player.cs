using UnityEngine;

public class Tiny_Player : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float attackRange = 1.6f;
    public int attackDamage = 10;
    public Transform attackPoint;
    public LayerMask enemyLayer;

    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer rbSprite;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        HandleMove();

        if (Input.GetMouseButtonDown(0))
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetTrigger("isAttack");
            HandleAttack();
        }
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
            //enemy.GetComponent<EnemyAI>().TakeDamage(attackDamage);
            Debug.Log("Damage dealt to enemy");
        }
    }

    //Display the attack range of the attack point
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
