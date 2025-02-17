using UnityEngine;

public class Player_map_2_Collision : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            gameManager.AddScore(100);
        }
        if (collision.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            gameManager.AddKey(1);
        }
    }
}
