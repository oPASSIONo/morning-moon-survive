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
    public event EventHandler OnAttackAction;
    public event EventHandler OnInteractionAction;
    public event EventHandler OnDashAction;

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
        
        playerInput.PlayerControls.Attack.performed += Attack_Performed;
        
        playerInput.PlayerControls.Interaction.performed += Interaction_Performed;
        playerInput.PlayerControls.Dash.performed += Dash_Performed;


    }

    /*private void OnDestroy()
    {
        playerInput.PlayerControls.Pause.performed -= Pause_Performed;
        playerInput.PlayerControls.Inventory.performed -= Inventory_Performed;
        playerInput.PlayerControls.Attack.performed -= Attack_Performed;

        playerInput.Dispose();
    }*/

    private void Pause_Performed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Inventory_Performed(InputAction.CallbackContext obj)
    {
        OnInventoryAction?.Invoke(this, EventArgs.Empty);
    }

    private void Attack_Performed(InputAction.CallbackContext obj)
    {
        OnAttackAction?.Invoke(this, EventArgs.Empty);
    }
    
    public void Interaction_Performed(InputAction.CallbackContext obj)
    {
        OnInteractionAction?.Invoke(this,EventArgs.Empty);
    }
    private void Dash_Performed(InputAction.CallbackContext obj)
    {
        OnDashAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovement()
    {
        Vector2 inputVector = playerInput.PlayerControls.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }

    
    
}
