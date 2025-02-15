using UnityEngine;

public class Tiny_Player : MonoBehaviour
{
    public float moveSpeed = 5f;
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
}
