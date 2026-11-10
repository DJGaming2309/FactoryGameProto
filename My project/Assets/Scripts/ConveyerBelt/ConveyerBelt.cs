
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [Header("Conveyor Settings")]
    public float beltSpeed = 2f;

    // Shared multiplier used by every conveyor.
    public static float speedMultiplier = 1f;

    [Header("Movement Points")]
    public Transform startPoint;
    public Transform endPoint;

    [Header("Direction Arrow")]
    public Transform directionArrow;

    private void Update()
    {
        UpdateArrowDirection();
    }

    private void UpdateArrowDirection()
    {
        if (startPoint == null || endPoint == null || directionArrow == null)
            return;

        Vector2 direction = ((Vector2)endPoint.position - (Vector2)startPoint.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        directionArrow.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        MoveProduct(other);
    }

    private void MoveProduct(Collider2D other)
    {
        Rigidbody2D productRb = other.attachedRigidbody;

        if (productRb == null || startPoint == null || endPoint == null)
            return;

        // Calculate the belt direction.
        Vector2 direction =
            ((Vector2)endPoint.position - (Vector2)startPoint.position).normalized;

        // Calculate the closest point on the belt's centre line.
        Vector2 productPosition = productRb.position;
        Vector2 start = startPoint.position;

        Vector2 closestPoint = start +
            Vector2.Dot(productPosition - start, direction) * direction;

        // Gradually correct sideways drifting.
        Vector2 correction = closestPoint - productPosition;

        // Apply conveyor speed and alignment correction.
        productRb.linearVelocity = (direction * beltSpeed * speedMultiplier) + (correction * 15f);
    }
}
