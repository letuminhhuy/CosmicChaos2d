using UnityEngine;

public class Player_map_2_AttackArea : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float damage = 2.5f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Player_map_2_Health>() != null)
        {
            Player_map_2_Health heath = collider.GetComponent<Player_map_2_Health>();
            heath.Damage(damage);
        }
    }
}
