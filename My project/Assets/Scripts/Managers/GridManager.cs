using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public float cellSize = 1f; //size of each grid cell in gameplay

    //converts a world position into the nearest grid position
    public Vector2 SnapToGrid(Vector2 worldPosition)
    {
        //rounds the X and Y positions to the nearest grid cell
        float snappedX = Mathf.Round(worldPosition.x / cellSize) * cellSize;
        float snappedY = Mathf.Round(worldPosition.y / cellSize) * cellSize;

        //returns the snapped position
        return new Vector2(snappedX, snappedY);
    }
}
