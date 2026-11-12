
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Abertay.Analytics;

public class GridPlacementPreview : MonoBehaviour
{
    //game objects to look at
    [Header("References")]
    public GridManager gridManager;
    public MoneyManager moneyManager;
    public Transform preview;
    public GameObject conveyorPrefab;

    //how much each conveyor costs
    [Header("Conveyor Cost")]
    public int conveyorCost = 10;

    private Camera mainCamera;
    private bool placementMode = false;

    //allows other scripts to check placement mode.
    public bool IsPlacementModeActive => placementMode;

    //current conveyor rotation.
    private float currentRotation = 0f;

    //keeps track of occupied grid cells.
    private HashSet<Vector2Int> occupiedCells = new HashSet<Vector2Int>();

    //starts by finding the camera and making sure the preview is not on
    void Start()
    {
        mainCamera = Camera.main;

        if (preview != null)
            preview.gameObject.SetActive(false);
    }

    void Update()
    {
        if (gridManager == null || moneyManager == null || preview == null || conveyorPrefab == null ||mainCamera == null)
            return;

        if (Keyboard.current == null || Mouse.current == null)
            return;

        //don't allow placement controls while the game is paused.
        if (Time.timeScale == 0f)
            return;

        //press B to toggle conveyor placement mode.
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            placementMode = !placementMode;
            preview.gameObject.SetActive(placementMode);
        }

        //do nothing else if placement mode is inactive.
        if (!placementMode)
            return;

        //press R to rotate the conveyor clockwise.
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            currentRotation -= 90f;
        }

        preview.rotation = Quaternion.Euler(0f, 0f, currentRotation);

        Vector3 mousePosition = Mouse.current.position.ReadValue();

        mousePosition.z = Mathf.Abs(mainCamera.transform.position.z - preview.position.z);

        Vector3 worldPosition =
            mainCamera.ScreenToWorldPoint(mousePosition);

        Vector2 snappedPosition =
            gridManager.SnapToGrid(worldPosition);

        preview.position = new Vector3(snappedPosition.x,snappedPosition.y,preview.position.z);

        //attempts to place a conveyor.
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2Int cell = new Vector2Int(
                Mathf.RoundToInt(snappedPosition.x / gridManager.cellSize),Mathf.RoundToInt(snappedPosition.y / gridManager.cellSize));

            if (occupiedCells.Contains(cell))
                return;

            if (moneyManager.SpendMoney(conveyorCost))
            {
                Instantiate(conveyorPrefab,new Vector3(snappedPosition.x, snappedPosition.y, 0f),Quaternion.Euler(0f, 0f, currentRotation));

                //tracks the conveyor placed
                SessionAnalytics.TrackConveyorPlaced();

                //records when a conveyor is successfully placed.
                AnalyticsManager.SendCustomEvent(
                    "ConveyorPlaced",
                    new Dictionary<string, object>
                    {
                        { "ConveyorCost", conveyorCost },
                        { "Rotation", currentRotation },
                        { "GridX", cell.x },
                        { "GridY", cell.y }
                    }
                );

                occupiedCells.Add(cell);
            }
            else
            {
                Debug.Log("Not enough money to place a conveyor!");
            }
        }
    }

    //called by the pause menu when Escape is pressed.
    public void CancelPlacementMode()
    {
        placementMode = false;

        if (preview != null)
            preview.gameObject.SetActive(false);
    }
}
