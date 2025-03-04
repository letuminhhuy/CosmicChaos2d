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
    public GameObject enemyPrefab;
    public float spawnDelay = 3.0f;
    //private int enemiesToSpawn = 5;

    private Vector2 startPosition;
    private bool isReturning = false;

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

        if ((direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight))
        {
            Flip();
        }

        if (distanceToPlayer <= chaseRange && !isReturning)
        {
            if (distanceToPlayer > attackRange)
            {
                rb.linearVelocity = direction * walkSpeed;
                animator.SetBool("isRun", true);
                animator.SetBool("isAttack", false);
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
            if (!isReturning)
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

    private IEnumerator ReturnToStartPosition()
    {
        animator.SetBool("isRun", true);

        while (Vector2.Distance(transform.position, startPosition) > 0.1f)
        {
            Vector2 direction = (startPosition - (Vector2)transform.position).normalized;
            if ((direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight))
            {
                Flip();
            }
            rb.linearVelocity = direction * walkSpeed;
            yield return null;
        }

        rb.linearVelocity = Vector2.zero;
        animator.SetBool("isRun", false);
        isReturning = false;
        animator.SetBool("isAttack", false);
        Flip();
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log("Pawn_Enemy đã chết!");
        Pawn_EnemySpawn spawner = FindObjectOfType<Pawn_EnemySpawn>();
        if (spawner != null)
        {
            spawner.StartSpawning(startPosition);
        }
    }
}
