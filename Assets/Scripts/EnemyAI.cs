using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float patrolRadius = 4f, walkSpeed = 2f;
    private Vector3[] trianglePoints;
    private int currentPointIndex = 0;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GenerateTrianglePoints();
    }

    void Update() => Patrol();

    void Patrol()
    {
        animator.SetBool("isWalk", true);

        if (Vector2.Distance(transform.position, trianglePoints[currentPointIndex]) < 0.2f)
            currentPointIndex = (currentPointIndex + 1) % 3;

        MoveTo(trianglePoints[currentPointIndex]);
    }

    void MoveTo(Vector3 target)
    {
        Vector2 direction = (target - transform.position).normalized;
        rb.linearVelocity = direction * walkSpeed;
    }

    void GenerateTrianglePoints()
    {
        trianglePoints = new Vector3[3];
        for (int i = 0; i < 3; i++)
            trianglePoints[i] = transform.position +
                                (Vector3)(Quaternion.Euler(0, 0, 120 * i) * Vector2.up * patrolRadius);
    }
}
