using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField] private float previewYOffset = 0.06f;
    
    [SerializeField] private GameObject cellIndicator;
    [SerializeField] private Grid grid;
    private GameObject previewObject;
    
    [SerializeField] private Material previewMaterialsPrefab;
    private Material previewMaterialInstance;

    private Renderer[] cellIndicatorRenderer;
  
    private int currentRotationAngle = 0; // Track current rotation angle



    private void Start()
    {
        previewMaterialInstance = new Material(previewMaterialsPrefab);
        cellIndicator.SetActive(false);
        cellIndicatorRenderer = cellIndicator.GetComponentsInChildren<Renderer>();
        
    }

    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        cellIndicator.transform.rotation = Quaternion.Euler(0,0,0);
        previewObject = Instantiate(prefab);
        PreparePreview(previewObject);
        PrepareCursor(size);
        cellIndicator.SetActive(true);
    }

    private void PrepareCursor(Vector2Int size)
    {
        if (size.x > 0 || size.y > 0)
        {
            cellIndicator.transform.localScale = new Vector3(size.x, 1, size.y);
            foreach (Renderer renderer in cellIndicatorRenderer)
            {
                renderer.material.mainTextureScale = size;
            } 
        }
    }

    private void PreparePreview(GameObject previewObject)
    {
        Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = previewMaterialInstance;
            }

            renderer.materials = materials;
        }
    }

    public void StopShowingPreview()
    {
        cellIndicator.SetActive(false);
        if (previewObject != null)
        {
            Destroy(previewObject);
        }

        currentRotationAngle = 0;
    }
    
    public void UpdatePosition(Vector3 position, bool validity, bool hasEnough)
    {
        if (previewObject != null)
        {
            MovePreview(position);
            ApplyFeedbackToPreview(validity, hasEnough);
        }
        MoveCursor(position);
        ApplyFeedbackToCursor(validity, hasEnough);
    }

    private void ApplyFeedbackToPreview(bool validity, bool hasEnough)
    {
        Color color;
    
        if (validity && hasEnough)
        {
            color = Color.green; // Valid placement and enough ingredients
        }
        else if (!validity)
        {
            color = Color.red; // Invalid placement
        }
        else
        {
            color = Color.yellow; // Valid placement but not enough ingredients
        }

        color.a = 0.5f;
        previewMaterialInstance.color = color;
    }

    private void ApplyFeedbackToCursor(bool validity, bool hasEnough)
    {
        Color color;

        if (validity && hasEnough)
        {
            color = Color.green; // Valid placement and enough ingredients
        }
        else if (!validity)
        {
            color = Color.red; // Invalid placement
        }
        else
        {
            color = Color.yellow; // Valid placement but not enough ingredients
        }

        color.a = 0.5f;
        foreach (Renderer renderer in cellIndicatorRenderer)
        {
            renderer.material.color = color;
        }
    }

    /*
    public void UpdatePosition(Vector3 position, bool validity)
    {
        if (previewObject != null)
        {
            MovePreview(position);
            ApplyFeedbackToPreview(validity);
        }
        MoveCursor(position);
        ApplyFeedbackToCursor(validity);
    }

    private void ApplyFeedbackToPreview(bool validity)
    {
        Color color = validity ? Color.green: Color.red;
        color.a = 0.5f;
        previewMaterialInstance.color = color;
    }
    
    private void ApplyFeedbackToCursor(bool validity)
    {
        Color color = validity ? Color.green : Color.red;
        color.a = 0.5f;
        foreach (Renderer renderer in cellIndicatorRenderer)
        {
            renderer.material.color = color;
        }
    }
    */
    
    public void ChangePreviewColor(Color color)
    {
        previewMaterialInstance.color = color;
        /*// Assuming you have a method to change the material color of the preview object
        previewObject.GetComponent<Renderer>().material.color = color;*/
    }

    private void MoveCursor(Vector3 position)
    {
        cellIndicator.transform.position = position;
    }

    private void MovePreview(Vector3 position)
    {
        previewObject.transform.position = new Vector3(position.x, position.y + previewYOffset, position.z);
    }
    
    public void StartShowingRemovePreview()
    {
        cellIndicator.SetActive(true);
        PrepareCursor(Vector2Int.one);
        ApplyFeedbackToCursor(false , false);
    }
    
    public void RotatePreview(int rotationAngle)
    {
        if (previewObject != null)
        {
            currentRotationAngle = (currentRotationAngle + rotationAngle) % 360;
            previewObject.transform.rotation = Quaternion.Euler(0, currentRotationAngle, 0);
            cellIndicator.transform.rotation = Quaternion.Euler(0, currentRotationAngle, 0);
            
            Debug.Log("Object Rotate : " + currentRotationAngle);
        }
    }
    public Vector3 GetCurrentPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3Int gridPosition = grid.WorldToCell(hit.point);
        
            return grid.CellToWorld(gridPosition);
        }

        return Vector3.zero;
    }

    public int GetCurrentRotation()
    {
        return currentRotationAngle;
    }
}
