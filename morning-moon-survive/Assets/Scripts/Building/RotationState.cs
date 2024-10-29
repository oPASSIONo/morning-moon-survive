using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationState : IBuildingState
{
    private Grid grid;
    private PreviewSystem previewSystem;
    private GridData floorData;
    private GridData furnitureData;
    private ObjectPlacer objectPlacer;

    private int selectedObjectIndex = -1;
    private int rotationIndex = 0; // To keep track of the rotation (90, 180, 270, etc.)

    public RotationState(Grid grid, 
        PreviewSystem previewSystem, 
        GridData floorData, 
        GridData furnitureData, 
        ObjectPlacer objectPlacer)
    {
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.floorData = floorData;
        this.furnitureData = furnitureData;
        this.objectPlacer = objectPlacer;
        
        previewSystem.StartShowingPlacementPreview(null, Vector2Int.one);  // Empty preview, only rotating
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        // When the user clicks, rotate the object at the given position.
        //RotateObject(gridPosition);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        // On each key press, rotate the preview object by 90 degrees
        if (Input.GetMouseButtonDown(1))
        {
            RotatePreviewObject();
        }

        // Update the position and rotation for the visual feedback
        Vector3 worldPosition = grid.CellToWorld(gridPosition);
        previewSystem.UpdatePosition(worldPosition, true , false);
    }

    private void RotatePreviewObject()
    {
        rotationIndex += 90;
        if (rotationIndex >= 360)
        {
            rotationIndex = 0;
        }
        
        previewSystem.RotatePreview(rotationIndex);
    }

    /*private void RotateObject(Vector3Int gridPosition)
    {
        // Find the object to rotate at the grid position (assuming it's furniture or other object)
        int gameObjectIndex = furnitureData.GetRepresentationIndex(gridPosition);

        if (gameObjectIndex == -1)
        {
            Debug.Log("No object found to rotate at this position.");
            return;
        }

        // Rotate the object in the game world
        GameObject objectToRotate = objectPlacer.GetObjectAt(gameObjectIndex);

        if (objectToRotate != null)
        {
            // Apply the new rotation to the object
            objectToRotate.transform.rotation = Quaternion.Euler(0, rotationIndex, 0);
        }
    }*/
}