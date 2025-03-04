using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float timeDestroy = 1f;


    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed ;
        Destroy(gameObject, timeDestroy);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Tiny_Player player = collision.GetComponent<Tiny_Player>();
        //if (player != null)
        //{
        //    player.TakeDamage(damage);
        //    Destroy(gameObject, 1f);
        //}
        if (collision.CompareTag("Player"))
        {
            Tiny_Player player = collision.GetComponent<Tiny_Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
                
            }
            Destroy(gameObject);
        }
    }
}
