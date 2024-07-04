using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;
    public LayerMask interactableLayerMask;

    private IInteractable currentInteractable;

    private void Update()
    {
        DetectInteractable();

        // Check for interaction using Input System
        if (Keyboard.current.eKey.wasPressedThisFrame && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    private void DetectInteractable()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayerMask))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null && interactable != currentInteractable)
            {
                if (currentInteractable != null)
                {
                    currentInteractable.HideInteractPrompt();
                }
                currentInteractable = interactable;
                currentInteractable.ShowInteractPrompt();
            }
        }
        else
        {
            if (currentInteractable != null)
            {
                currentInteractable.HideInteractPrompt();
                currentInteractable = null;
            }
        }
    }
}