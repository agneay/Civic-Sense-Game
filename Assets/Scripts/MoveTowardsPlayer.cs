using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float stopDistance = 1.5f;

    [Header("Detection")]
    public float detectionDistance = 15f;
    public LayerMask playerLayer;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, detectionDistance, playerLayer))
        {
            if (hit.transform.root.CompareTag("Player"))
            {
                float distanceToPlayer = hit.distance;

                if (distanceToPlayer > stopDistance)
                {
                    Vector3 move = transform.forward * moveSpeed * Time.fixedDeltaTime;
                    rb.MovePosition(rb.position + move);
                }
                else
                {
                    rb.linearVelocity = Vector3.zero;
                }
            }
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * detectionDistance);
    }
}