using UnityEngine;

public class Player_map_2_AttackArea : MonoBehaviour
{
    private float damage = 25f;
    private bool isAttacking = false; // Biến kiểm tra có đang tấn công không
    private SpriteRenderer playerSpriteRenderer;
    void Start()
    {
        // Lấy SpriteRenderer của Player từ đối tượng cha
        playerSpriteRenderer = GetComponentInParent<SpriteRenderer>();
    }
    public void SetAttacking(bool attacking)
    {
        isAttacking = attacking;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Player_map_2_Health>() != null)
        {
            Player_map_2_Health heath = collider.GetComponent<Player_map_2_Health>();
            heath.Damage(damage);
        }
        if (!isAttacking) return; // Chỉ gây sát thương nếu đang tấn công

        if (collider.CompareTag("Enemy") || collider.CompareTag("Boss"))
        {
            EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
            // Kiểm tra hướng tấn công
            Vector3 directionToEnemy = (collider.transform.position - transform.position).normalized;

            if (IsFacingTarget(directionToEnemy))
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log("Player tấn công Boss: " + damage);
            }
            else
            {
                Debug.Log("Tấn công không thành công: Không đối diện mục tiêu.");
            }
        }
    }
    // Hàm kiểm tra hướng của Player có đối diện với kẻ địch không
    private bool IsFacingTarget(Vector3 directionToEnemy)
    {
        // Kiểm tra hướng của SpriteRenderer (flipX xác định Player đang quay trái hay phải)
        if (playerSpriteRenderer.flipX && directionToEnemy.x < 0) // Player quay trái và mục tiêu bên trái
        {
            return true;
        }
        else if (!playerSpriteRenderer.flipX && directionToEnemy.x > 0) // Player quay phải và mục tiêu bên phải
        {
            return true;
        }
        return false; // Nếu không đối diện
    }
}