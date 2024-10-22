using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

public class PlacementState : IBuildingState
{
    private int selectedObjectIndex = -1;
    private int ID;
    private Grid grid;
    private PreviewSystem previewSystem;
    private BuildingObjectSo database;
    private GridData floorData;
    private GridData furnitureData;
    private ObjectPlacer objectPlacer;
    private InventoryController inventoryController; // Add reference to InventoryController


    public PlacementState(int id,
        Grid grid,
        PreviewSystem previewSystem,
        BuildingObjectSo database,
        GridData floorData,
        GridData furnitureData,
        ObjectPlacer objectPlacer,
        InventoryController inventoryController)
    {
        ID = id;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.database = database;
        this.floorData = floorData;
        this.furnitureData = furnitureData;
        this.objectPlacer = objectPlacer;
        this.inventoryController = inventoryController;
        
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex > -1)
        {
            previewSystem.StartShowingPlacementPreview(
                database.objectsData[selectedObjectIndex].Prefab,
                database.objectsData[selectedObjectIndex].Size);
        }
        else
        {
            throw new System.Exception($"No object with ID {id} ");
        }
        
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        
        // Check if there are enough ingredients
        if (!inventoryController.HasEnoughIngredients(database.objectsData[selectedObjectIndex].Recipe.RequiredIngredients))
        {
            // Change the preview to indicate insufficient ingredients
            previewSystem.UpdatePosition(previewSystem.GetCurrentPosition(), false, false); // Update the preview with invalid placement
            Debug.Log("Not enough ingredients to place the object.");
            return; // Prevent placement
        }
        
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false)
        {
            return;
        }
        // Apply rotation to the placed object
        int currentRotation = previewSystem.GetCurrentRotation();
        GameObject placedObject = database.objectsData[selectedObjectIndex].Prefab;
        int index = objectPlacer.PlaceObject(placedObject,  grid.CellToWorld(gridPosition),currentRotation);
        
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? floorData : furnitureData;
        selectedData.AddObjectAt(gridPosition, 
            database.objectsData[selectedObjectIndex].Size, 
            database.objectsData[selectedObjectIndex].ID,
            index,
            currentRotation);
        
        // Remove ingredients after successful placement
        inventoryController.RemoveIngredients(database.objectsData[selectedObjectIndex].Recipe.RequiredIngredients);

        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false , false);
    }
    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex )
    {
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? floorData : furnitureData;

        return selectedData.CanPlaceObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size ,previewSystem.GetCurrentRotation());
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
    
        // Check if there are enough ingredients
        bool hasEnoughIngredients = inventoryController.HasEnoughIngredients(database.objectsData[selectedObjectIndex].Recipe.RequiredIngredients);
    
        // Update the preview with both placement validity and ingredient availability
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity, hasEnoughIngredients);
        
        /*bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);*/
      
    }
}
