
using UnityEngine;
using UnityEngine.InputSystem;

public class ProductDrag : MonoBehaviour
{
    private Vector3 offset; // Difference between mouse and product position
    private Vector3 targetPosition; // Where the product should move

    private Camera mainCamera; // Reference to the main camera
    private Rigidbody2D rb; // Product physics component

    private bool isDragging = false; // Checks if the product is being dragged

    private RigidbodyType2D originalBodyType; // Stores the original physics type

    [Header("Throwing Physics")]
    public float throwMultiplier = 1.5f; // Controls how strong the throw is
    public float maxThrowSpeed = 12f; // Maximum speed when releasing a product

    private Vector2 previousPosition; // Previous mouse world position
    private Vector2 dragVelocity; // Movement speed while dragging

    // Runs when the game starts
    void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;

        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Store the original physics type
        if (rb != null)
        {
            originalBodyType = rb.bodyType;
        }

        // Set the starting target position
        targetPosition = transform.position;
    }

    // Runs when the player clicks the product
    void OnMouseDown()
    {
        if (Mouse.current == null)
            return;

        // Get the mouse position in the game world
        Vector3 mousePosition = GetMouseWorldPosition();

        // Calculate the offset so the product doesn't jump
        offset = transform.position - mousePosition;

        // Stop existing movement while picking up the product
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
        }

        // Store the starting mouse position
        previousPosition = mousePosition;

        // Reset the drag velocity
        dragVelocity = Vector2.zero;

        // Start dragging
        isDragging = true;
    }

    // Runs while the player holds and moves the mouse
    void OnMouseDrag()
    {
        if (!isDragging)
            return;

        // Get the current mouse position
        Vector3 mousePosition = GetMouseWorldPosition();

        // Calculate how quickly the mouse is moving
        dragVelocity = ((Vector2)mousePosition - previousPosition) / Time.deltaTime;

        // Store the current mouse position for the next frame
        previousPosition = mousePosition;

        // Update the target position
        targetPosition = mousePosition + offset;

        // Keep the product inside the factory boundaries
        targetPosition.x = Mathf.Clamp(targetPosition.x, -11.5f, 11.5f);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -4.5f, 5.5f);
    }

    //Runs during the physics update
    void FixedUpdate()
    {
        //Move the product smoothly while dragging
        if (isDragging && rb != null)
        {
            rb.MovePosition(targetPosition);
        }

        //Keep the product inside the factory boundaries
        if (!isDragging)
        {
            Vector2 currentPosition = rb.position;

            currentPosition.x = Mathf.Clamp(currentPosition.x, -11.5f, 11.5f);
            currentPosition.y = Mathf.Clamp(currentPosition.y, -4.5f, 5.5f);

            //Apply the corrected position
            rb.position = currentPosition;
        }
    }

    //Runs when the player releases the mouse
    void OnMouseUp()
    {
        // Stop dragging
        isDragging = false;

        // Restore the original physics type
        if (rb != null)
        {
            rb.bodyType = originalBodyType;

            // Calculate the throwing velocity
            Vector2 throwVelocity = dragVelocity * throwMultiplier;

            // Prevent excessively fast throws
            throwVelocity = Vector2.ClampMagnitude(
                throwVelocity,
                maxThrowSpeed
            );

            // Apply the movement when releasing the product
            rb.linearVelocity = throwVelocity;
        }
    }

    // Gets the mouse position relative to the product's depth
    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();

        float distance = Mathf.Abs(
            mainCamera.transform.position.z - transform.position.z
        );

        mousePosition.z = distance;

        return mainCamera.ScreenToWorldPoint(mousePosition);
    }
}
