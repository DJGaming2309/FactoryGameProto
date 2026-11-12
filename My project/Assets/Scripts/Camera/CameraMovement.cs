
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed = 8f; //how quickly the camera moves

    [Header("Camera Boundaries")]
    public float minX = -5f; //furthest left the camera can move
    public float maxX = 5f; //furthest right the camera can move
    public float minY = -3f; //furthest down the camera can move
    public float maxY = 3f; //furthest up the camera can move

    private Vector2 movement; //stores the movement direction

    //runs every frame
    void Update()
    {
        //resets the movement direction
        movement = Vector2.zero;

        //checks which keys are being pressed
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

        //prevents the diagonal movement from being faster
        movement = movement.normalized;

        //calculate the new camera position
        Vector3 newPosition = transform.position;

        newPosition.x += movement.x * cameraSpeed * Time.deltaTime;
        newPosition.y += movement.y * cameraSpeed * Time.deltaTime;

        //keeps the camera inside the factory boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        //applies the new camera position
        transform.position = newPosition;
    }
}
