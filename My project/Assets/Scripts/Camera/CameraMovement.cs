
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed = 8f; // How quickly the camera moves

    private Vector2 movement; // Stores the movement direction

    // Runs every frame
    void Update()
    {
        // Reset the movement direction
        movement = Vector2.zero;

        // Check which keyboard keys are pressed
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed)
                movement.y = 1f;

            if (Keyboard.current.sKey.isPressed)
                movement.y = -1f;

            if (Keyboard.current.aKey.isPressed)
                movement.x = -1f;

            if (Keyboard.current.dKey.isPressed)
                movement.x = 1f;
        }

        // Prevent diagonal movement from being faster
        movement = movement.normalized;

        // Move the camera
        transform.position += new Vector3(
            movement.x,
            movement.y,
            0f
        ) * cameraSpeed * Time.deltaTime;
    }
}
