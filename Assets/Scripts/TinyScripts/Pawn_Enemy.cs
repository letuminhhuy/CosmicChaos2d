using System.Collections;
using UnityEngine;

public class Pawn_Enemy : Enemy
{
    public Tiny_Player player;
    public float walkSpeed = 0.5f, runSpeed = 2f;
    public float chaseRange = 2.5f;
    public float attackRange = 0.5f;
    public float enterDamage = 0.5f;
    public float stayDamage = 0.0f;
    public float spawnDelay = 3.0f;

    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<Tiny_Player>();
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

        if ((direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight))
        {
            Flip();
        }

        if (distanceToPlayer <= chaseRange)
        {
            if (distanceToPlayer > attackRange)
            {
                MoveToPlayer(direction);
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
                animator.SetBool("isRun", false);
                animator.SetBool("isAttack", true);
                animator.SetTrigger("isAttack");
                player.TakeDamage(stayDamage);
            }
        }
        else
        {
            animator.SetBool("isAttack", false);
            MoveToPlayer(direction);
        }
    }

    private void MoveToPlayer(Vector2 direction)
    {
        Vector2 newPosition = rb.position + direction * walkSpeed * Time.fixedDeltaTime;
        rb.linearVelocity = direction * walkSpeed;
        animator.SetBool("isRun", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enemy va chạm với: " + collision.gameObject.name);
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                Debug.Log("Pawn bị tấn công");
                player.TakeDamage(enterDamage);
                TakeDamage(enterDamage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                Debug.Log("Pawn bị tấn công");
                player.TakeDamage(stayDamage);
                TakeDamage(stayDamage);
            }
        }
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log("Pawn_Enemy đã chết!");
    }
}