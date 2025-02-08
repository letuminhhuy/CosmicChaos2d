using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speedPlatForm = 3f;
    private Vector3 target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = pointA.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speedPlatForm * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            if (target == pointA.position)
            {
                target = pointB.position;
            }
            else 
            {
                target = pointA.position; 
            }
        }
    }
}
