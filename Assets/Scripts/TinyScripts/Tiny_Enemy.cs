using UnityEngine;
using UnityEngine.UI;

public class Tiny_Enemy : MonoBehaviour
{
    public Tiny_Player player;
    public float walkSpeed = 1f, runSpeed = 4f;
    public float chaseRange = 5f; // Phạm vi đuổi theo
    public float attackRange = 1f; // Phạm vi tấn công

    [SerializeField] float maxHP = 100f;
    protected float currentHP;
    [SerializeField] private Image hpBar;

    [SerializeField] protected float enterDamage = 10f;
    [SerializeField] protected float stayDamage = 1f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
        UpdateHPBar();
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            // Nếu người chơi trong phạm vi đuổi theo
            if (distanceToPlayer <= chaseRange)
            {
                // Đuổi theo người chơi
                Vector2 direction = (player.transform.position - transform.position).normalized;
                rb.linearVelocity = direction * walkSpeed;

                // Nếu người chơi trong phạm vi tấn công
                if (distanceToPlayer <= attackRange)
                {
                    // Tấn công người chơi
                    player.TakeDamage(stayDamage);
                }
            }
            else
            {
                // Dừng di chuyển khi ra khỏi phạm vi đuổi theo
                rb.linearVelocity = Vector2.zero;
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

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);
        UpdateHPBar();
        if (currentHP <= 0)
        {
            Debug.Log("Enemy was died.");
            // Thêm logic xử lý khi kẻ địch chết (vd: hủy đối tượng)
            Destroy(gameObject);
        }
    }

    private void UpdateHPBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHP / maxHP;
        }
    }
}
