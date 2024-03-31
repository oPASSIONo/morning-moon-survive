using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    // Reference to the InventoryManager
    //public InventoryManager inventoryManager;

    // Callback method for opening the inventory
    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //inventoryManager.OpenInventory();
        }
    }

    // Callback method for interacting with objects
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Implement interaction logic here
        }
    }
}