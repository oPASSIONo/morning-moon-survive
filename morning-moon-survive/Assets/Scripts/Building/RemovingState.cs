using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingState : IBuildingState
{
    private int gameObjectIndex = -1;
    private int ID;
    private Grid grid;
    private PreviewSystem previewSystem;
    private GridData floorData;
    private GridData furnitureData;
    private ObjectPlacer objectPlacer;

    public RemovingState(Grid grid, 
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
        
        previewSystem.StartShowingRemovePreview();
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        GridData selectedData = null;
        
        int rotationAngle = previewSystem.GetCurrentRotation();
        
        if (furnitureData.CanPlaceObjectAt(gridPosition, Vector2Int.one , rotationAngle) == false)
        {
            selectedData = furnitureData;
        }
        else if(floorData.CanPlaceObjectAt(gridPosition, Vector2Int.one , rotationAngle) == false)
        {
            selectedData = floorData;
        }

        if (selectedData == null)
        {
            // Add sound here
            Debug.Log("No object found to remove.");

        }
        else
        {
            gameObjectIndex = selectedData.GetRepresentationIndex(gridPosition);
            if (gameObjectIndex == -1)
            {
                return;
            }

            selectedData.RemoveObjectAt(gridPosition);
            objectPlacer.RemoveObjectAt(gameObjectIndex);
        }

        Vector3 cellPosition = grid.CellToWorld(gridPosition);
        previewSystem.UpdatePosition(cellPosition , CheckIfSelectionIsValid(gridPosition) ,false);

    }

    private bool CheckIfSelectionIsValid(Vector3Int gridPosition)
    {
        // Using the current rotation angle to check if the selection is valid
        int rotationAngle = previewSystem.GetCurrentRotation();
        return !(furnitureData.CanPlaceObjectAt(gridPosition, Vector2Int.one, rotationAngle) &&
                 floorData.CanPlaceObjectAt(gridPosition, Vector2Int.one, rotationAngle));
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool validity = CheckIfSelectionIsValid(gridPosition);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), validity ,false);
    }
}
