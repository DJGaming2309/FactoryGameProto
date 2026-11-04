
using UnityEngine;
using UnityEngine.InputSystem;

public class ProductDrag : MonoBehaviour
{
    private Vector3 offset; //Difference between mouse and product position
    private Vector3 targetPosition; //Where the product should move
    private Camera mainCamera; //Reference to the main camera
    private Rigidbody2D rb; //Product physics component

    private bool isDragging = false; //Checks if the product is being dragged
    private float originalGravity; //Stores the original gravity setting
    private RigidbodyType2D originalBodyType; //Stores the original physics type

    //Runs when the game starts
    void Start()
    {
        //Get the main camera
        mainCamera = Camera.main;

        //Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        //Store the original physics settings
        if (rb != null)
        {
            originalGravity = rb.gravityScale;
            originalBodyType = rb.bodyType;
        }

        targetPosition = transform.position;
    }

    //Runs when the player clicks the product
    void OnMouseDown()
    {
        if (Mouse.current == null)
            return;

        //Get the mouse position in the game world
        Vector3 mousePosition = GetMouseWorldPosition();

        //Calculate the offset so the product doesn't jump
        offset = transform.position - mousePosition;

        //Stop physics from fighting against dragging
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
        }

        //Start dragging the product
        isDragging = true;
    }

    //Runs while the player holds and moves the mouse
    void OnMouseDrag()
    {
        if (!isDragging)
            return;

        //Update the target position using the moving camera
        targetPosition = GetMouseWorldPosition() + offset;
    }

    //Runs during the physics update
    void FixedUpdate()
    {
        //Move the product smoothly while dragging
        if (isDragging && rb != null)
        {
            rb.MovePosition(targetPosition);
        }
    }

    //Runs when the player releases the mouse
    void OnMouseUp()
    {
        //Stop dragging
        isDragging = false;

        //Restore the original physics settings
        if (rb != null)
        {
            rb.bodyType = originalBodyType;
            rb.gravityScale = originalGravity;
            rb.linearVelocity = Vector2.zero;
        }
    }

    //Gets the mouse position relative to the product's depth
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
