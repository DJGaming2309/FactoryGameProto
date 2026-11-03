
using UnityEngine;
using UnityEngine.InputSystem;

public class ProductDrag : MonoBehaviour
{
    private Vector3 offset; //Stores the difference between the mouse and product position
    private Camera mainCamera; //Stores the main camera
    private Rigidbody2D rb; //Stores the product's physics component

    private bool isDragging = false; //Checks if the product is currently being dragged
    private float originalGravity; //Stores the product's original gravity setting

    //Runs when the game starts
    void Start()
    {
        //Get the main camera
        mainCamera = Camera.main;

        //Get the product's Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        //Store the original gravity setting
        if (rb != null)
        {
            originalGravity = rb.gravityScale;
        }
    }

    //Runs when the player clicks the product
    void OnMouseDown()
    {
        //Get the mouse position in the game world
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(
            Mouse.current.position.ReadValue()
        );

        //Calculate the offset so the product doesn't jump to the mouse
        offset = transform.position - new Vector3(mousePosition.x, mousePosition.y, transform.position.z);

        //Turn off gravity while the product is being dragged
        if (rb != null)
        {
            rb.gravityScale = 0;

            //Stop any existing movement
            rb.linearVelocity = Vector2.zero;
        }

        //Set dragging to true
        isDragging = true;
    }

    //Runs while the player holds and moves the mouse
    void OnMouseDrag()
    {
        //Only move the product if it is being dragged
        if (!isDragging)
            return;

        //Get the current mouse position
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(
            Mouse.current.position.ReadValue());

        //Move the product to follow the mouse
        transform.position = new Vector3
            (mousePosition.x, mousePosition.y, transform.position.z) + offset;
    }

    //Runs when the player releases the mouse button
    void OnMouseUp()
    {
        //Set dragging to false
        isDragging = false;

        //Restore the original gravity setting
        if (rb != null)
        {
            rb.gravityScale = originalGravity;
        }
    }
}
