using UnityEngine;

public class Boss : MonoBehaviour
{
    public float patrolDistance = 3f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float attackRange = 1.5f;
    public float attackCooldown = 2f; // Thời gian hồi chiêu tấn công
    public Transform player;

    private Animator animator;
    private Vector3 startPosition;
    private Vector3[] patrolPoints;
    private int currentPointIndex = 0;
    private bool isChasing = false;
    private bool isAttacking = false;
    private float lastAttackTime = 0f;
    private SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHealth;

    void Start()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();

        patrolPoints = new Vector3[4] {
            startPosition + Vector3.left * patrolDistance,
            startPosition + Vector3.up * patrolDistance,
            startPosition + Vector3.right * patrolDistance,
            startPosition + Vector3.down * patrolDistance
        };
    }

    void Update()
    {
        // Nếu Boss chết thì không làm gì nữa
        if (enemyHealth.IsDead()) return;

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        Vector3 targetPosition = patrolPoints[currentPointIndex];
        Vector3 direction = (targetPosition - transform.position).normalized;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, patrolSpeed * Time.deltaTime);

        UpdateSpriteDirection(direction);
        //animator.SetBool("isMoving", true); // Dùng animation di chuyển

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            isAttacking = false;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isChasing", true);

            Vector3 direction = (player.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);

            UpdateSpriteDirection(direction);
        }
        else
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        if (Time.time - lastAttackTime >= attackCooldown) // Kiểm tra hồi chiêu
        {
            isAttacking = true;
            animator.SetBool("isAttacking", true);
            lastAttackTime = Time.time; // Lưu thời gian tấn công
            player.GetComponent<Player_map_2_Health>().Damage(15);
        }
    }

    void UpdateSpriteDirection(Vector3 direction)
    {
        if (direction.x > 0)
            spriteRenderer.flipX = false;
        else if (direction.x < 0)
            spriteRenderer.flipX = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyHealth.IsDead()) return; // Nếu Boss chết thì không làm gì
        if (collision.CompareTag("Player"))
        {
            isChasing = true;
            animator.SetBool("isChasing", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enemyHealth.IsDead()) return;
        if (collision.CompareTag("Player"))
        {
            isChasing = false;
            isAttacking = false;
            animator.SetBool("isChasing", false);
            animator.SetBool("isAttacking", false);
        }
        Invoke(nameof(CheckAndResetHealth), 3f);
    }
    private void CheckAndResetHealth()
    {
        if (!isChasing && gameObject.CompareTag("Boss"))
        {
            enemyHealth.ResetHealth();
        }
    }
}
