using UnityEngine;

public class HealItem : MonoBehaviour
{
    public int healAmount = 20;
    private bool canBePickedUp = false;

    void Start()
    {
        Invoke("AllowPickup", 1f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canBePickedUp)
        {
            Tiny_Player player = collision.GetComponent<Tiny_Player>();
            if (player != null)
            {
                player.Heal(healAmount);
                Destroy(gameObject);
            }
        }
    }

    void AllowPickup()
    {
        canBePickedUp = true;
    }
}
