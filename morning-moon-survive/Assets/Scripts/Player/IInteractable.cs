using UnityEngine;

public interface IInteractable
{
    void Interact(GameObject player); // Pass player reference
    void ShowInteractPrompt();
    void HideInteractPrompt();
}