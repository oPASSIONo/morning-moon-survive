using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFarmingInteractor : MonoBehaviour
{
    [SerializeField] private float rayDistance = 1f;              // The maximum distance of the raycast.
    [SerializeField] private LayerMask interactableLayer;         // Layer mask for filtering raycast hits.
    [SerializeField] private Color rayColor = Color.green;        // Color of the raycast line in the Scene view.

    private Transform _cachedTransform;                           // Cached reference to the player's transform for performance.
    private Land selectedLand = null;                             // Reference to the currently selected land.

    private void Awake()
    {
        // Cache the transform component for performance optimization.
        _cachedTransform = transform;
    }

    private void Update()
    {
        PerformRaycast();
    }

    // Perform raycast to detect interactable objects
    private void PerformRaycast()
    {
        RaycastHit hit;
        Vector3 rayOrigin = _cachedTransform.position;            // Use cached transform to get the player's position.
        Vector3 rayDirection = _cachedTransform.forward;          // The ray will cast in the forward direction of the player.

        // Perform raycast against the specified interactable layer
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayDistance, interactableLayer))
        {
            HandleRaycastHit(hit);
        }
        else
        {
            DeselectCurrentLand();                                // Deselect land if nothing is hit.
        }

        // Visualize the ray in the Scene view for debugging purposes.
        Debug.DrawRay(rayOrigin, rayDirection * rayDistance, rayColor);
    }

    // Handle interaction when the ray hits an interactable object
    private void HandleRaycastHit(RaycastHit hit)
    {
        Collider other = hit.collider;

        // Check if the hit object is tagged as "Land"
        if (other.CompareTag("Land"))
        {
            Land land = other.GetComponent<Land>();
            SelectLand(land);
        }
    }

    // Select a new piece of land, deselecting the previous one if applicable
    private void SelectLand(Land land)
    {
        if (selectedLand != land)                                 // Only select a new land if it's not already selected.
        {
            DeselectCurrentLand();                                // Deselect previously selected land.
            selectedLand = land;
            selectedLand.Select(true);                            // Mark the new land as selected.
        }
    }

    // Deselect the currently selected land if any
    private void DeselectCurrentLand()
    {
        if (selectedLand != null)
        {
            selectedLand.Select(false);                           // Deselect the land.
            selectedLand = null;                                  // Clear the reference.
        }
    }
}
