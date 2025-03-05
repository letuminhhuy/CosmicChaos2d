using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player_map_2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Speed of player 

    [SerializeField] private AudioClip slashSound; // Âm thanh chém
    private AudioSource audioSource;

    private Rigidbody2D Rigidbody; // Physics of player
    private SpriteRenderer spriteRenderer; // Reference to sprite renderer
    private Animator animator; // Reference to animator
    private GameObject attackArea = default;
    private GameManager gameManager;
    private float timer = 0f;

    private bool attacking = false;
    private float timeToAttack = 0.8f;


    private bool canMove = true; // Controls whether player can move

    private GameObject healthBar;//Thanh máu
    private Tilemap groundTilemap; // Thêm tham chiếu đến Tilemap Ground


    [System.Obsolete]
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>(); // Reference to Rigidbody
        spriteRenderer = GetComponent<SpriteRenderer>(); // Reference to sprite renderer
        animator = GetComponent<Animator>(); // Reference to animator
        //gameManager = GetComponent<GameManager>();
        gameManager = FindAnyObjectByType<GameManager>();
        //attackArea = transform.GetChild(0).gameObject;
        attackArea = transform.Find("AttackArea")?.gameObject;
        healthBar = transform.Find("HealthBar")?.gameObject;
        // Tìm Tilemap Ground trong scene
        groundTilemap = GameObject.Find("Ground").GetComponent<Tilemap>(); // Thay "Ground" bằng tên GameObject của Tilemap Ground
        // Âm thanh
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (gameManager.IsGameOver() || gameManager.IsGameWin())
        {
            return;
        }
        if (canMove)
        {
            MovePlayer();        }
        else
        {
            Rigidbody.linearVelocity = Vector2.zero; // Stop movement during attacks or when dead
        }

        HandleAttack();
        ClampPlayerPosition(); // Giới hạn vị trí Player
    }
    void ClampPlayerPosition()
    {
        if (groundTilemap != null)
        {
            // Lấy bounds của Tilemap Ground
            Bounds bounds = groundTilemap.localBounds;
            Vector3 min = groundTilemap.transform.TransformPoint(bounds.min);
            Vector3 max = groundTilemap.transform.TransformPoint(bounds.max);

            // Giới hạn vị trí Player trong bounds của Ground
            Vector3 position = transform.position;
            position.x = Mathf.Clamp(position.x, min.x, max.x);
            position.y = Mathf.Clamp(position.y, min.y, max.y);
            transform.position = position;
        }
    }
    void MovePlayer()
    {
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //Rigidbody.linearVelocity = playerInput.normalized * moveSpeed; // Updated to use `velocity` instead of `linearVelocity`       
        Rigidbody.linearVelocity = playerInput.normalized * moveSpeed;
        FlipPlayer(playerInput);
        AnimationPlayerRun(playerInput);
    }

    void FlipPlayer(Vector2 player)
    {
        // Chỉ thay đổi flipX khi hướng đi thay đổi, tránh bị lặp lại
        if (player.x != 0)
        {
            spriteRenderer.flipX = player.x < 0;
        }
    }


    void AnimationPlayerRun(Vector2 player)
    {
        animator.SetBool("Player_IsRun", player != Vector2.zero);
    }

    void HandleAttack()
    {
        if (!attacking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartAttack("Player_IsAttack_1");
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
    /*void HandleAttack()
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
    }*/

    void StartAttack(string attackTrigger)
    {
        attacking = true;
        canMove = false;
        timer = 0;
        animator.SetTrigger(attackTrigger);
        attackArea.SetActive(true);
        // Bật trạng thái tấn công khi Player chém
        attackArea.GetComponent<Player_map_2_AttackArea>().SetAttacking(true);
        Rigidbody.linearVelocity = Vector2.zero; // Stop movement during attack

        // Phát âm thanh chém
        PlaySound(slashSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    void EndAttack()
    {
        attacking = false;
        canMove = true;
        attackArea.SetActive(false);
        // Tắt trạng thái tấn công sau khi kết thúc đòn chém
        attackArea.GetComponent<Player_map_2_AttackArea>().SetAttacking(false);
        ResetAttackTriggers();

       
    }

    void ResetAttackTriggers()
    {
        animator.ResetTrigger("Player_IsAttack_1");
    }

    public bool IsMoving()
    {
        return Rigidbody.linearVelocity.magnitude > 0;
    }
    public bool IsFacingLeft()
    {
        return spriteRenderer.flipX;
    }


}