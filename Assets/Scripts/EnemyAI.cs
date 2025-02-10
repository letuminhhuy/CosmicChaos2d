using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public Player player;
    public float patrolRadius = 4f, chaseRadius = 20f, walkSpeed = 1f, runSpeed = 4f;
    private Vector3 patrolCenter;

    private Vector3[] patrolPoints;
    private int index = 0;
    
    private bool facingRight = true;
    private bool isChasing = false;
    private bool isReturning = false;

    [SerializeField] float maxHP = 100f;
    protected float currentHP;
    [SerializeField] private Image hpBar;

    [SerializeField] protected float enterDamage = 10f;
    [SerializeField] protected float stayDamage = 1f;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        patrolCenter = transform.position;
        GeneratePlusPoints();

        currentHP = maxHP;
        UpdateHPBar();
    }

    void Update()
    {
        Vector2 playerOffset = player.transform.position - patrolCenter;
        float playerDistFromPatrolCenter = playerOffset.magnitude;

        if (playerDistFromPatrolCenter <= patrolRadius)
        {
            isChasing = true;
            isReturning = false;
        }
        else if (playerDistFromPatrolCenter > chaseRadius)
        {
            isChasing = false;
            isReturning = true;
        }

        if (isChasing)
            ChasePlayer();
        else if (isReturning)
            ReturnToPatrol();
        else
            Patrol();

        FlipDirection();
    }

    //Di chuyển tuần tra theo hình dấu "+"
    void Patrol()
    {
        animator.SetBool("isRun", false);
        animator.SetBool("isWalk", true);

        if (Vector2.Distance(transform.position, patrolPoints[index]) < 0.2f)
            index = (index + 1) % patrolPoints.Length;

        MoveTo(patrolPoints[index], walkSpeed);
    }

    void ChasePlayer()
    {
        animator.SetBool("isRun", true);
        animator.SetBool("isWalk", false);

        MoveTo(player.transform.position, runSpeed);
    }

    void ReturnToPatrol()
    {
        animator.SetBool("isRun", true);
        animator.SetBool("isWalk", false);

        if (Vector2.Distance(transform.position, patrolCenter) > 0.1f)
        {
            MoveTo(patrolCenter, runSpeed);
        }
        else
        {
            isReturning = false;
            animator.SetBool("isRun", false);
        }
    }

    void MoveTo(Vector3 target, float speed)
    {
        Vector2 direction = (target - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    void FlipDirection()
    {
        if ((rb.linearVelocity.x > 0 && !facingRight) || (rb.linearVelocity.x < 0 && facingRight))
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    // Tạo 5 điểm di chuyển theo hình dấu "+"
    void GeneratePlusPoints()
    {
        patrolPoints = new Vector3[5];
        patrolPoints[0] = patrolCenter;                                // Trung tâm
        patrolPoints[1] = patrolCenter + Vector3.up * patrolRadius;    // Trên
        patrolPoints[2] = patrolCenter + Vector3.down * patrolRadius;  // Dưới
        patrolPoints[3] = patrolCenter + Vector3.left * patrolRadius;  // Trái
        patrolPoints[4] = patrolCenter + Vector3.right * patrolRadius; // Phải
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                player.TakeDamege(enterDamage);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                player.TakeDamege(stayDamage);
            }
        }
    }


    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);
        UpdateHPBar();
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void UpdateHPBar()
    {
        if(hpBar != null)
        {
            hpBar.fillAmount = currentHP / maxHP;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
