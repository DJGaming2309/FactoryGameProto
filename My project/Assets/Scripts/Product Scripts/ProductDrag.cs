
using UnityEngine;
using UnityEngine.InputSystem;

public class ProductDrag : MonoBehaviour
{
    private Vector3 offset; //difference between mouse and product position
    private Vector3 targetPosition; //where the product should move

    private Camera mainCamera; //reference to the main camera
    private Rigidbody2D rb; //product physics component

    private bool isDragging = false; //checks if the product is being dragged

    private RigidbodyType2D originalBodyType; //stores the original physics type

    [Header("Throwing Physics")]
    public float throwMultiplier = 1.5f; //controls how strong the throw is
    public float maxThrowSpeed = 12f; //maximum speed when releasing a product

    private Vector2 previousPosition; //previous mouse world position
    private Vector2 dragVelocity; //movement speed while dragging

    //runs when the game starts
    void Start()
    {
        //gets the main camera
        mainCamera = Camera.main;

        //gets the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        //stores the original physics type
        if (rb != null)
        {
            originalBodyType = rb.bodyType;
        }

        //Sets the starting target position
        targetPosition = transform.position;
    }

    //runs when the player clicks the product
    void OnMouseDown()
    {
        if (Mouse.current == null)
            return;

        //gets the mouse position in the game world
        Vector3 mousePosition = GetMouseWorldPosition();

        //calculates the offset so the product doesn't jump
        offset = transform.position - mousePosition;

        //stops existing movement while picking up the product
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
        }

        //stores the starting mouse position
        previousPosition = mousePosition;

        //resets the drag velocity
        dragVelocity = Vector2.zero;

        //starts dragging
        isDragging = true;
    }

    //runs while the player holds and moves the mouse
    void OnMouseDrag()
    {
        if (!isDragging)
            return;

        //gets the current mouse position
        Vector3 mousePosition = GetMouseWorldPosition();

        //calculates how quickly the mouse is moving
        dragVelocity = ((Vector2)mousePosition - previousPosition) / Time.deltaTime;

        //stores the current mouse position for the next frame
        previousPosition = mousePosition;

        //updates the target position
        targetPosition = mousePosition + offset;

        //keeps the product inside the factory boundaries
        targetPosition.x = Mathf.Clamp(targetPosition.x, -11.5f, 11.5f);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -4.5f, 5.5f);
    }

    //runs during the physics update
    void FixedUpdate()
    {
        //moves the product smoothly while dragging
        if (isDragging && rb != null)
        {
            rb.MovePosition(targetPosition);
        }

        //keeps the product inside the factory boundaries
        if (!isDragging)
        {
            Vector2 currentPosition = rb.position;

            currentPosition.x = Mathf.Clamp(currentPosition.x, -11.5f, 11.5f);
            currentPosition.y = Mathf.Clamp(currentPosition.y, -4.5f, 5.5f);

            //applies the corrected position
            rb.position = currentPosition;
        }
    }

    //runs when the player releases the mouse
    void OnMouseUp()
    {
        //stops dragging
        isDragging = false;

        //restores the original physics type
        if (rb != null)
        {
            rb.bodyType = originalBodyType;

            //calculates the throwing velocity
            Vector2 throwVelocity = dragVelocity * throwMultiplier;

            //prevents excessively fast throws
            throwVelocity = Vector2.ClampMagnitude(throwVelocity,maxThrowSpeed);

            //applies the movement when releasing the product
            rb.linearVelocity = throwVelocity;
        }
    }

    //gets the mouse position relative to the product's depth
    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();

        float distance = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);

        mousePosition.z = distance;

        return mainCamera.ScreenToWorldPoint(mousePosition);
    }
}
