using System.Collections;
using UnityEngine;

public class Tiny_Enemy : Enemy
{
    public Tiny_Player player;
    public float walkSpeed = 1f;
    public float chaseRange = 5f;
    public float attackRange = 1f; 
    public float enterDamage = 1f;
    public float stayDamage = 0f;

    private Vector2 startPosition;
    private bool isReturning = false; // Trạng thái đang quay về vị trí ban đầu

    public GameObject healItem;

    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<Tiny_Player>();
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

        if (distanceToPlayer <= chaseRange && !isReturning)
        {
            if (distanceToPlayer > attackRange) 
            {
                Vector2 newPosition = rb.position + direction * walkSpeed * Time.fixedDeltaTime;
                rb.linearVelocity = direction * walkSpeed;
                animator.SetBool("isRun", true);
                animator.SetBool("isAttack", false);
            }
            else 
            {
                rb.linearVelocity = Vector2.zero;
                animator.SetBool("isRun", false);
                animator.SetBool("isAttack", true);
                //animator.SetTrigger("isAttack");

                player.TakeDamage(stayDamage);
            }
        }
        else 
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
                Debug.Log("va cham");
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
                Debug.Log("va cham");
                player.TakeDamage(stayDamage);
            }
        }
    }

    private IEnumerator ReturnToStartPosition()
    {
        animator.SetBool("isRun", true); 

        while (Vector2.Distance(transform.position, startPosition) > 0.1f)
        {
            Vector2 direction = (startPosition - (Vector2)transform.position).normalized;

            // Kiểm tra và lật hướng nếu cần khi quay về
            if ((direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight))
            {
                Flip();
            }

            Vector2 newPosition = rb.position + direction * walkSpeed * Time.fixedDeltaTime;
            rb.linearVelocity = direction * walkSpeed;
            yield return null;
        }

        rb.linearVelocity = Vector2.zero;
        animator.SetBool("isRun", false); 
        isReturning = false;

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
        if (healItem != null & Random.value <0.5f)
        {
            Instantiate(healItem, transform.position, Quaternion.identity);
        }
        
    }
}