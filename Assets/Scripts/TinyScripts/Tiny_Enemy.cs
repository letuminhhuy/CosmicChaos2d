using System.Collections;
using UnityEngine;

public class Tiny_Enemy : Enemy
{
    public Tiny_Player player;
    public float walkSpeed = 1f, runSpeed = 4f;
    public float chaseRange = 5f; // Phạm vi đuổi theo
    public float attackRange = 1f; // Phạm vi tấn công
    public float enterDamage = 10f;
    public float stayDamage = 1f;

    private Vector2 startPosition;
    private bool isReturning = false; // Trạng thái đang quay về vị trí ban đầu

    protected override void Start()
    {
        base.Start();
        startPosition = transform.position;
    }

    private void Update()
    {
        if (player == null)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isRun", false);
            animator.SetBool("isAttack", false);
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Kiểm tra và lật hướng khi di chuyển
        if ((direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight))
        {
            Flip();
        }

        if (distanceToPlayer <= chaseRange && !isReturning) // Nếu Player trong phạm vi đuổi theo và Enemy chưa quay về vị trí gốc
        {
            if (distanceToPlayer > attackRange) // Nếu Player ở vùng đuổi nhưng ngoài vùng tấn công
            {
                rb.linearVelocity = direction * walkSpeed;
                animator.SetBool("isRun", true);
                animator.SetBool("isAttack", false);
            }
            else // Nếu Player trong vùng tấn công
            {
                rb.linearVelocity = Vector2.zero;
                animator.SetBool("isRun", false);
                animator.SetBool("isAttack", true);
                animator.SetTrigger("isAttack");

                player.TakeDamage(stayDamage);
            }
        }
        else // Nếu Player ra khỏi phạm vi chaseRange HOẶC ra khỏi attackRange khi đang bị tấn công
        {
            animator.SetBool("isAttack", false);

            if (!isReturning) // Nếu chưa quay về vị trí ban đầu
            {
                isReturning = true;
                StartCoroutine(ReturnToStartPosition());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                player.TakeDamage(enterDamage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                player.TakeDamage(stayDamage);
            }
        }
    }

    private IEnumerator ReturnToStartPosition()
    {
        animator.SetBool("isRun", true); // Bật animation chạy khi quay về

        while (Vector2.Distance(transform.position, startPosition) > 0.1f)
        {
            Vector2 direction = (startPosition - (Vector2)transform.position).normalized;

            // Kiểm tra và lật hướng nếu cần khi quay về
            if ((direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight))
            {
                Flip();
            }

            rb.linearVelocity = direction * walkSpeed;
            yield return null;
        }

        rb.linearVelocity = Vector2.zero;
        animator.SetBool("isRun", false); // Tắt animation chạy khi về đến nơi
        isReturning = false;

        // không tấn công khi quay về
        if (Vector2.Distance(transform.position, player.transform.position) > attackRange)
        {
            animator.SetBool("isAttack", false);
        }

        Flip();
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log("Tiny_Enemy đã chết!");
    }
}