using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector3[] verticalWaypoints; // Define las coordenadas verticales en el Inspector

    private Rigidbody2D rb;
    private int currentWaypointIndex = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveHorizontal();
        MoveVertical();
    }

    void MoveHorizontal()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(horizontalInput, 0f);
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }

    void MoveVertical()
    {
        if (Input.GetKeyDown(KeyCode.W) && currentWaypointIndex < verticalWaypoints.Length - 1)
        {
            currentWaypointIndex++;
            MoveToWaypoint();
        }
        else if (Input.GetKeyDown(KeyCode.S) && currentWaypointIndex > 0)
        {
            currentWaypointIndex--;
            MoveToWaypoint();
        }
    }

    void MoveToWaypoint()
    {
        Vector3 targetPosition = verticalWaypoints[currentWaypointIndex];
        transform.position = new Vector3(transform.position.x, targetPosition.y, transform.position.z);
    }
}
