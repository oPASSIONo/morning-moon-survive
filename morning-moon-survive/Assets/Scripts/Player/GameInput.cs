using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages player inputs using the Input System.
/// </summary>
public class GameInput : MonoBehaviour
{
    
    public static GameInput Instance { get; private set; }
    private PlayerInput playerInput;
    private InputAction pause;

    public event EventHandler OnPauseAction;
    public event EventHandler OnInventoryAction;
    public event EventHandler OnAction;
    public event EventHandler OnInteractionAction;
    public event EventHandler OnDashAction;
    
    public event EventHandler<int> OnSelectSlotAction;
    public InputAction[] slotSelectActions { get; private set; }
    public const int NumberOfSlots = 10; // Adjust the number of slots as needed


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        playerInput = new PlayerInput();
        playerInput.PlayerControls.Enable();

        playerInput.PlayerControls.Pause.performed += Pause_Performed;

        playerInput.PlayerControls.Inventory.performed += Inventory_Performed;
        
        playerInput.PlayerControls.Action.performed += Action_Performed;
        
        playerInput.PlayerControls.Interaction.performed += Interaction_Performed;
        playerInput.PlayerControls.Dash.performed += Dash_Performed;

        slotSelectActions = new InputAction[NumberOfSlots];

        for (int i = 0; i < NumberOfSlots; i++)
        {
            int slotIndex = i; // Local copy for the lambda
            slotSelectActions[i] = playerInput.FindAction($"SelectSlot{slotIndex + 1}");
            if (slotSelectActions[i] != null)
            {
                slotSelectActions[i].performed += context => OnSelectSlotAction?.Invoke(this, slotIndex);
            }
            else
            {
                Debug.LogWarning($"Input action 'SelectSlot{slotIndex + 1}' not found.");
            }
        }

    }

    private void Pause_Performed(InputAction.CallbackContext obj)
    {
        PlayerStateManager.Instance.TogglePause();
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Inventory_Performed(InputAction.CallbackContext obj)
    {
        PlayerStateManager.Instance.ToggleInventory();
        OnInventoryAction?.Invoke(this, EventArgs.Empty);
    }

    private void Action_Performed(InputAction.CallbackContext obj)
    {
        if (PlayerStateManager.Instance.currentState == PlayerStateManager.PlayerState.Normal)
        {
            OnAction?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Interaction_Performed(InputAction.CallbackContext obj)
    {
        if (PlayerStateManager.Instance.currentState == PlayerStateManager.PlayerState.Normal)
        {
            OnInteractionAction?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Dash_Performed(InputAction.CallbackContext obj)
    {
        if (PlayerStateManager.Instance.currentState == PlayerStateManager.PlayerState.Normal)
        {
            OnDashAction?.Invoke(this, EventArgs.Empty);
        }
    }

    public Vector2 GetMovement()
    {
        if (PlayerStateManager.Instance.currentState == PlayerStateManager.PlayerState.Normal)
        {
            Vector2 inputVector = playerInput.PlayerControls.Move.ReadValue<Vector2>();
            return inputVector.normalized;
        }
        return Vector2.zero;
    }

    public void SetPlayerInput(bool isEnable)
    {
        if (isEnable)
        {
            playerInput.PlayerControls.Enable();
        }
        else
        {
            playerInput.PlayerControls.Disable();
        }
    }
    
}
