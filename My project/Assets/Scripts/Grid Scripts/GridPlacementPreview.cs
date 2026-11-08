
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class GridPlacementPreview : MonoBehaviour
{
    [Header("References")]
    public GridManager gridManager;
    public Transform preview;
    public GameObject conveyorPrefab;

    private Camera mainCamera;
    private bool placementMode = false;

    // Keeps track of grid cells that already have a belt
    private HashSet<Vector2Int> occupiedCells = new HashSet<Vector2Int>();

    void Start()
    {
        mainCamera = Camera.main;

        // Hide the preview until placement mode is activated
        if (preview != null)
            preview.gameObject.SetActive(false);
    }

    void Update()
    {
        // Check that all required references exist
        if (gridManager == null || preview == null ||
            conveyorPrefab == null || mainCamera == null)
            return;

        if (Keyboard.current == null || Mouse.current == null)
            return;

        // Press B to toggle conveyor placement mode
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            placementMode = !placementMode;
            preview.gameObject.SetActive(placementMode);
        }

        // Press Escape to exit placement mode
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            placementMode = false;
            preview.gameObject.SetActive(false);
        }

        // Do nothing else if placement mode is inactive
        if (!placementMode)
            return;

        // Get the mouse position
        Vector3 mousePosition = Mouse.current.position.ReadValue();

        // Convert the mouse position into world coordinates
        mousePosition.z = Mathf.Abs(
            mainCamera.transform.position.z - preview.position.z
        );

        Vector3 worldPosition =
            mainCamera.ScreenToWorldPoint(mousePosition);

        // Snap the position to the grid
        Vector2 snappedPosition =
            gridManager.SnapToGrid(worldPosition);

        // Move the preview to the snapped position
        preview.position = new Vector3(
            snappedPosition.x,
            snappedPosition.y,
            preview.position.z
        );

        // Place a belt when the left mouse button is clicked
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Calculate which grid cell the mouse is over
            Vector2Int cell = new Vector2Int(
                Mathf.RoundToInt(snappedPosition.x / gridManager.cellSize),
                Mathf.RoundToInt(snappedPosition.y / gridManager.cellSize)
            );

            // Only place a belt if the cell is empty
            if (!occupiedCells.Contains(cell))
            {
                Instantiate(
                    conveyorPrefab,
                    new Vector3(snappedPosition.x, snappedPosition.y, 0f),
                    Quaternion.identity
                );

                // Mark this cell as occupied
                occupiedCells.Add(cell);
            }
        }
    }
}
