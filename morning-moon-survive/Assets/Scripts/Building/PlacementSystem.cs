using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private BuildInputManager inputManager;

    [SerializeField] private Grid grid;

    [SerializeField] private BuildingObjectSo database;

    [SerializeField] private GameObject gridVisualization;
    //[SerializeField] private AudioSource source; // Add sound
    
    private GridData floorData, furnitureData;

    [SerializeField] private PreviewSystem preview;
    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    [SerializeField] private ObjectPlacer objectPlacer;

    private IBuildingState buildingState;
    
    private void Start()
    {
        StopPlacement();
        floorData = new();
        furnitureData = new();
    }

    public void StartPlacement(int ID )
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new PlacementState(ID, grid, preview, database, floorData, furnitureData, objectPlacer);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    public void StartRemoving()
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new RemovingState(grid, preview, floorData, furnitureData, objectPlacer);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
        
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointOverUI())
        {
            return;
        }
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

       buildingState.OnAction(gridPosition);
    }

    /*private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex )
    {
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? floorData : furnitureData;

        return selectedData.CanPlaceObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
    }*/

    private void StopPlacement()
    {
        if (buildingState == null)
        {
            return;
        }
        gridVisualization.SetActive(false);
        buildingState.EndState();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
        buildingState = null;
    }

    private void Update()
    {
        if (buildingState == null)
        {
            return;
        }
        
        // Check for rotation input
        if (Input.GetMouseButtonDown(1))
        {
            preview.RotatePreview(90); // Rotate by 90 degrees on 'Q'
        }
        
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        if (lastDetectedPosition != gridPosition)
        {
           buildingState.UpdateState(gridPosition);
           lastDetectedPosition = gridPosition;
        }
   
    }
}
