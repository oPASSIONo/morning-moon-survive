using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    private Dictionary<Vector3Int, PlacementData> placedObjects = new();

    public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectSize, int ID, int placedObjectIndex, int rotationAngle)
    {
        Vector2Int rotatedSize = GetRotatedSize(objectSize, rotationAngle);

        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, rotatedSize, rotationAngle);
        
        PlacementData data = new PlacementData(positionToOccupy, ID, placedObjectIndex);
        
        foreach (var pos in positionToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
            {
                throw new Exception($"Dictionary already contains this cell position {pos}");
            }
            placedObjects[pos] = data;
        }
    }
    
    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize, int rotationAngle)
    {
        List<Vector3Int> returnValue = new();
        
        // Rotate objectSize based on the rotation angle
        Vector2Int rotatedSize = GetRotatedSize(objectSize, rotationAngle);
        
        for (int x = 0; x < rotatedSize.x; x++)
        {
            for (int y = 0; y < rotatedSize.y; y++)
            {
                // Calculate positions based on rotation
                Vector3Int offset = GetRotatedOffset(x, y, rotationAngle);
                returnValue.Add(gridPosition + offset);
            }
        }
        return returnValue;
        
    }
    
    // Method to rotate size based on the rotation angle
    private Vector2Int GetRotatedSize(Vector2Int size, int rotationAngle)
    {
        if (rotationAngle == 90 || rotationAngle == 270)
        {
            // Swap the width and height if rotated 90 or 270 degrees
            return new Vector2Int(size.y, size.x);
        }
        return size; // No change if 0 or 180 degrees
    }

    // Method to calculate the position offset based on rotation
    private Vector3Int GetRotatedOffset(int x, int y, int rotationAngle)
    {
        switch (rotationAngle)
        {
            case 90:
                return new Vector3Int(y, 0, -x); // Rotate 90 degrees clockwise
            case 180:
                return new Vector3Int(-x, 0, -y); // Rotate 180 degrees
            case 270:
                return new Vector3Int(-y, 0, x); // Rotate 270 degrees clockwise
            default:
                return new Vector3Int(x, 0, y); // No rotation (0 degrees)
        }
    }

    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize, int rotationAngle)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize, rotationAngle);

        foreach (var pos in positionToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
            {
                return false;
            }
        }
        return true;
    }

    public int GetRepresentationIndex(Vector3Int gridPosition)
    {
        if (placedObjects.ContainsKey(gridPosition) == false)
        {
            return -1;
        }

        return placedObjects[gridPosition].PlacedObjectIndex;
    }

    public void RemoveObjectAt(Vector3Int gridPosition)
    {
        /*foreach (var pos in placedObjects[gridPosition].occupiedPositions)
        {
            placedObjects.Remove(pos);
           
        }*/
        
        if (placedObjects.TryGetValue(gridPosition, out PlacementData placementData))
        {
            foreach (var pos in placementData.occupiedPositions)
            {
                placedObjects.Remove(pos);
            }
        }
    }
}

public class PlacementData
{
    
    public List<Vector3Int> occupiedPositions;

    public int ID { get; private set; }
    public int PlacedObjectIndex { get; private set; }
    

    public PlacementData(List<Vector3Int> occupiedPositions, int id, int placedObjectIndex)
   {
       this.occupiedPositions = occupiedPositions;
       ID = id;
       PlacedObjectIndex = placedObjectIndex;
   }
}