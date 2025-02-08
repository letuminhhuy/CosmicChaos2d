using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private LayerMask groundLayer; // Mặt đất
    [SerializeField] private Transform groundCheck;//Để check Player chạm mặt đất
    private bool isGrounded;
    private Rigidbody2D Rigidbody2D;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");//(x và y, có giá trị Dương hoặc Âm)
        Rigidbody2D.linearVelocity = new Vector2(moveInput * moveSpeed, Rigidbody2D.linearVelocity.y);

        //Lật nv
        if (moveInput >0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void HandleJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer); // Check nhân vật chạm đất hay chưa

        if (Input.GetButtonDown("Jump") && isGrounded)//Unity hiểu là nhảy là Space
        {
            Rigidbody2D.linearVelocity = new Vector2(Rigidbody2D.linearVelocity.x, jumpForce);
        }
    }
}
