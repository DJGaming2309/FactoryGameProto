
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed = 8f; // How quickly the camera moves

    [Header("Camera Boundaries")]
    public float minX = -5f; // Furthest left the camera can move
    public float maxX = 5f; // Furthest right the camera can move
    public float minY = -3f; // Furthest down the camera can move
    public float maxY = 3f; // Furthest up the camera can move

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

        // Calculate the new camera position
        Vector3 newPosition = transform.position;

        newPosition.x += movement.x * cameraSpeed * Time.deltaTime;
        newPosition.y += movement.y * cameraSpeed * Time.deltaTime;

        // Keep the camera inside the factory boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Apply the new camera position
        transform.position = newPosition;
    }
}
