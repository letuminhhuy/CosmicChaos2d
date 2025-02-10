using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer rbSprite;
    private Animator animator;

    [SerializeField]
    private float maxHp = 100f;
    private float currentHp;
    [SerializeField]
    private Image hpBar;


    private int coinCount = 0; // số coin thu nhập
    private int maxCoins = 6; // giới hạn
    [SerializeField] private TMP_Text coinText; // UI hiển thị số coin 


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        currentHp = maxHp;
        UpdateHpBar();
    }

    void Update()
    {
        MovePlayer();
        if (Input.GetMouseButtonDown(0))
        {
            Attack();   
        }
    }
    void MovePlayer()
    {
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.linearVelocity = playerInput.normalized * moveSpeed;
        if (playerInput.x > 0)
        {
            rbSprite.flipX = false;
        }
        else if (playerInput.x < 0)
        {
            rbSprite.flipX = true;
        }

        if (playerInput != Vector2.zero)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }

    }
    public virtual void TakeDame(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        UpdateHpBar();
        if (currentHp <= 0)
        {
            Die();
        }

    }
    private void Die()
    {
        Destroy(gameObject);
    }
    protected void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHp / maxHp; // thanh hp

        }
    }

    void Attack()
    {
        animator.SetTrigger("attack"); 
    }


    // Xử lí va chạm với coin
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            if (coinCount < maxCoins) // Kiểm tra nếu chưa thu thập đủ 6 coin
            {
                coinCount++;
                UpdateCoinText(); // Cập nhật số lượng coin trên UI
                Destroy(other.gameObject);
            }
        }
    }

    // Cập nhật UI hiển thị số lượng coin
    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount + "/" + maxCoins;
        }
    }

}
