using UnityEngine;

public class Player_map_2_AttackArea : MonoBehaviour
{
    /*  // Start is called once before the first execution of Update after the MonoBehaviour is created
      private float damage = 2.5f;

      private void OnTriggerEnter2D(Collider2D collider)
      {
          if (collider.GetComponent<Player_map_2_Health>() != null)
          {
              Player_map_2_Health heath = collider.GetComponent<Player_map_2_Health>();
              heath.Damage(damage);
          }
      }*/
    private float damage = 25f;
    private bool isAttacking = false; // Biến kiểm tra có đang tấn công không

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
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log("Player tấn công Boss: " + damage);
            }
        }
    }
}
