using UnityEngine;

public class Player_map_2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Speed of player 
    private Rigidbody2D Rigidbody; // Physics of player
    private SpriteRenderer spriteRenderer; // Reference to sprite renderer
    private Animator animator; // Reference to animator
    private GameObject attackArea = default;
    private GameManager gameManager;
    private float timer = 0f;

    private bool attacking = false;
    private float timeToAttack = 0.8f;


    private bool canMove = true; // Controls whether player can move


    [System.Obsolete]
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>(); // Reference to Rigidbody
        spriteRenderer = GetComponent<SpriteRenderer>(); // Reference to sprite renderer
        animator = GetComponent<Animator>(); // Reference to animator
        gameManager = GetComponent<GameManager>();
        gameManager = FindObjectOfType<GameManager>();
        attackArea = transform.GetChild(0).gameObject;
    }

    void Update()
    {

        if (gameManager.IsGameOver() || gameManager.IsGameWin())
        {
            return;
        }
        if (canMove)
        {
            MovePlayer();
        }
        else
        {
            Rigidbody.linearVelocity = Vector2.zero; // Stop movement during attacks or when dead
        }

        HandleAttack();
    }

    void MovePlayer()
    {
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Rigidbody.linearVelocity = playerInput.normalized * moveSpeed; // Updated to use `velocity` instead of `linearVelocity`
        FlipPlayer(playerInput);
        AnimationPlayerRun(playerInput);
    }

    void FlipPlayer(Vector2 player)
    {
        spriteRenderer.flipX = player.x < 0;
    }

    void AnimationPlayerRun(Vector2 player)
    {
        animator.SetBool("Player_IsRun", player != Vector2.zero);
    }

    void HandleAttack()
    {
        if (!attacking)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                StartAttack("Player_IsAttack_1");
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                StartAttack("Player_IsAttack_2");
            }
        }

        if (attacking)
        {
            timer += Time.deltaTime;
            if (timer >= timeToAttack)
            {
                EndAttack();
            }
        }
    }

    void StartAttack(string attackTrigger)
    {
        attacking = true;
        canMove = false;
        timer = 0;
        animator.SetTrigger(attackTrigger);
        attackArea.SetActive(true);
        Rigidbody.linearVelocity = Vector2.zero; // Stop movement during attack
    }

    void EndAttack()
    {
        attacking = false;
        canMove = true;
        attackArea.SetActive(false);
        ResetAttackTriggers();
    }

    void ResetAttackTriggers()
    {
        animator.ResetTrigger("Player_IsAttack_1");
        animator.ResetTrigger("Player_IsAttack_2");
    }


}