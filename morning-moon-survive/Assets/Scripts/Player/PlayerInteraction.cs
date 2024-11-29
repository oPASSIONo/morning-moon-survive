using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles player interaction with nearby interactable objects.
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    /// <summary>
    /// The maximum distance within which the player can interact with objects.
    /// </summary>
    [SerializeField] private float interactionDistance = 3f;

    /// <summary>
    /// The layer mask used to identify interactable objects.
    /// </summary>
    [SerializeField] private LayerMask interactableLayerMask;

    private IInteractable currentInteractable;

    private void Awake()
    {
        GameInput.Instance.OnInteractionAction += GameInput_OnInteractionAction;
    }

    private void GameInput_OnInteractionAction(object sender, EventArgs e)
    {
        DetectInteractable();
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    /// <summary>
    /// Detects interactable objects within the interaction distance using a sphere cast.
    /// </summary>
    private void DetectInteractable()
    {
        // Detect all colliders within the interaction distance that are on the interactable layer
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionDistance, interactableLayerMask);

        IInteractable nearestInteractable = null;
        float nearestDistance = interactionDistance;

        // Find the nearest interactable object
        foreach (var hitCollider in hitColliders)
        {
            IInteractable interactable = hitCollider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestInteractable = interactable;
                    nearestDistance = distance;
                }
            }
        }

        // If a new interactable object is detected, update the current interactable and show the prompt
        if (nearestInteractable != currentInteractable)
        {
            if (currentInteractable != null)
            {
                currentInteractable.HideInteractPrompt();
            }
            currentInteractable = nearestInteractable;
            if (currentInteractable != null)
            {
                currentInteractable.ShowInteractPrompt();
            }
        }
    }

    /// <summary>
    /// Visualizes the interaction range in the Unity Editor.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}
