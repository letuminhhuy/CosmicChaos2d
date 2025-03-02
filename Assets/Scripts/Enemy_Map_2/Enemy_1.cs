using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    public Transform pointA; // Điểm tuần tra A
    public Transform pointB; // Điểm tuần tra B
    public float patrolSpeed = 2f; // Tốc độ tuần tra
    public float chaseSpeed = 3.5f; // Tốc độ đuổi theo Player
    public float attackRange = 1f; // Khoảng cách tấn công
    public Transform player; // Đối tượng Player
    private Vector3 targetPosition; // Vị trí mục tiêu của Enemy
    private bool isChasing = false; // Trạng thái đuổi theo Player
    private SpriteRenderer spriteRenderer;
    private Vector3 startPosition; // Lưu vị trí Enemy khi Player rời khỏi vùng

    private Animator animator;
    private float attackCooldown = 1f; // Thời gian giữa các lần tấn công
    private float lastAttackTime = 0f;
    private EnemyHealth enemyHealth;
    private Player_map_2_Health playerHealth; // Tham chiếu đến máu của Player

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetPosition = pointA.position; // Enemy bắt đầu từ PointA
        startPosition = transform.position; // Lưu vị trí ban đầu
                                            // Tìm script Player_map_2_Health từ Player
        enemyHealth = GetComponent<EnemyHealth>();
        if (player != null)
        {
            playerHealth = player.GetComponent<Player_map_2_Health>();
        }
    }

    void Update()
    {
        if (enemyHealth.IsDead()) return; // Nếu chết thì dừng mọi hoạt động
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
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, patrolSpeed * Time.deltaTime);

        // Lật hướng sprite theo hướng di chuyển
        spriteRenderer.flipX = targetPosition.x < transform.position.x;

        // Khi Enemy đến 1 điểm tuần tra, đổi hướng di chuyển
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = (targetPosition == pointA.position) ? pointB.position : pointA.position;
        }
        
    }

    void ChasePlayer()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Enemy sẽ chỉ di chuyển nếu chưa đến phạm vi attackRange
        if (distanceToPlayer > attackRange)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Vector3 targetPosition = player.position - directionToPlayer * attackRange; // Lùi ra attackRange

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, chaseSpeed * Time.deltaTime);

            // Lật hướng sprite theo hướng di chuyển
            spriteRenderer.flipX = player.position.x < transform.position.x;
           
        }

        // Nếu Enemy đã vào vùng attackRange, nó sẽ gây sát thương liên tục
        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
    }


    private void AttackPlayer()
    {
        if (Time.time - lastAttackTime >= attackCooldown) // Kiểm tra cooldown tấn công
        {
            player.GetComponent<Player_map_2_Health>().Damage(5); // Giảm 5 máu Player
            lastAttackTime = Time.time; // Cập nhật thời gian tấn công
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyHealth.IsDead()) return;
        if (collision.CompareTag("Player"))
        {
            isChasing = true; // Bắt đầu đuổi theo Player
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isChasing = false; // Player rời khỏi vùng, Enemy quay lại tuần tra
        }
    }
    public void Die()
    {
        animator.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false; // Vô hiệu hóa va chạm
        this.enabled = false; // Tắt script Enemy_1
    }
}
