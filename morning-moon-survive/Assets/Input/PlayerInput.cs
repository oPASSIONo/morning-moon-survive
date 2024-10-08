//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""PlayerControls"",
            ""id"": ""dae4e22d-1269-4550-9177-004d0c29b49e"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""cb82695f-9fa3-4d3c-8a6d-5209c41aab55"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""719ab3a7-7d11-4512-b1eb-8a54ae32e011"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""95be0c09-5a0f-4f7b-8760-2fb80dbc84b4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""a74a1eae-41e1-4c8b-a8de-2525c69c23e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectSlot1"",
                    ""type"": ""Button"",
                    ""id"": ""9af2da4a-cc0f-4f25-b1ff-014b8bc7d5ae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectSlot2"",
                    ""type"": ""Button"",
                    ""id"": ""571fe157-9d95-4f22-b696-3855f1043d77"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectSlot3"",
                    ""type"": ""Button"",
                    ""id"": ""4b682041-cb05-44aa-a03c-7e99a4f01b81"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectSlot4"",
                    ""type"": ""Button"",
                    ""id"": ""8faf15a8-348c-4fce-addf-01db28440afb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectSlot5"",
                    ""type"": ""Button"",
                    ""id"": ""dd31eb96-b020-44de-a464-e4bbaaa89f7e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectSlot6"",
                    ""type"": ""Button"",
                    ""id"": ""471d71e2-e783-485b-85cc-f6922d4b88e5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectSlot7"",
                    ""type"": ""Button"",
                    ""id"": ""f73c923f-8581-41f3-8b2f-0c6b440d2be7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectSlot8"",
                    ""type"": ""Button"",
                    ""id"": ""2b2f5838-241e-42f0-a59d-d977e96c627a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectSlot9"",
                    ""type"": ""Button"",
                    ""id"": ""3d75ade0-d0ab-4102-8f97-5f9145def35c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectSlot10"",
                    ""type"": ""Button"",
                    ""id"": ""ea16ef1f-ce0e-43a3-a75a-6cf9652a736b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Action"",
                    ""type"": ""Button"",
                    ""id"": ""f10403c0-542e-440b-b25c-0d186e6d1c0d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DrawWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""dfb577f8-5885-48bb-8c3d-0f7a3aab32d3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SheathWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""754495d8-d954-4040-9c22-d9eabbe5fbc0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""6f448770-a8f2-423c-999a-2f422afdcdb3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Crafting"",
                    ""type"": ""Button"",
                    ""id"": ""48273641-91e7-40c1-9950-4c8194dc1afc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Building"",
                    ""type"": ""Button"",
                    ""id"": ""a78fd677-2cf4-4bf0-b07b-1b6b2b208e64"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""6dd2cd23-0300-43bc-99e9-14f9e3a93be3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""10f5b536-a635-4a2a-97f8-d440c5457239"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f0460c1f-96e8-4065-9330-6b53189fd580"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0ef62154-e3ca-484a-aaf6-c6e1b9f8ba99"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""61b2d672-4325-4a5a-8372-e1ce91da65b5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8d92edac-0f41-42f1-aa22-21d9395e672b"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e4b87ac-cf63-443c-9f2c-f74a681bb8ba"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""793c31fe-e78f-4b93-9cc5-8b9465562d07"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7a956317-7a55-46d2-bcae-c286b80c2bac"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectSlot1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""053d496e-9db7-4b9f-87e9-0ddac3879db0"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectSlot2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08be15ac-3b3d-4bcb-ae6a-849a2d9b69af"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectSlot3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8dff8476-cf25-4c1c-9b7d-9b20cb1ef3ac"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectSlot4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b4ef4d4-846b-4d59-b9fb-3f81204480c9"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectSlot5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b2abbaf-32db-476f-bd8d-22c7298cb19a"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectSlot6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b398156-d71d-45bd-99f4-f8e23d6dcc23"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectSlot7"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1a1f2fe3-938f-4950-be5a-8cac8844f614"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectSlot8"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82016b44-73b6-4ed0-8e31-16ceed3445d3"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectSlot9"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1cd9fd1b-c342-4f7e-8b9a-f3c6c1a40c5e"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectSlot10"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cd326953-5461-449e-b99d-5b89c622d04e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""93c7591b-092f-44c6-8565-127425babd9f"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DrawWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1901aa77-ef94-4f90-9289-8438dbc66291"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SheathWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad94e61a-fac2-46d6-ac1f-4ca95afeec98"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0eeb76b0-a0a7-4d2e-8f49-b25c4d45dda3"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crafting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a21150f-c147-4c86-becc-985ed921a249"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Building"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerControls
        m_PlayerControls = asset.FindActionMap("PlayerControls", throwIfNotFound: true);
        m_PlayerControls_Move = m_PlayerControls.FindAction("Move", throwIfNotFound: true);
        m_PlayerControls_Inventory = m_PlayerControls.FindAction("Inventory", throwIfNotFound: true);
        m_PlayerControls_Pause = m_PlayerControls.FindAction("Pause", throwIfNotFound: true);
        m_PlayerControls_Interaction = m_PlayerControls.FindAction("Interaction", throwIfNotFound: true);
        m_PlayerControls_SelectSlot1 = m_PlayerControls.FindAction("SelectSlot1", throwIfNotFound: true);
        m_PlayerControls_SelectSlot2 = m_PlayerControls.FindAction("SelectSlot2", throwIfNotFound: true);
        m_PlayerControls_SelectSlot3 = m_PlayerControls.FindAction("SelectSlot3", throwIfNotFound: true);
        m_PlayerControls_SelectSlot4 = m_PlayerControls.FindAction("SelectSlot4", throwIfNotFound: true);
        m_PlayerControls_SelectSlot5 = m_PlayerControls.FindAction("SelectSlot5", throwIfNotFound: true);
        m_PlayerControls_SelectSlot6 = m_PlayerControls.FindAction("SelectSlot6", throwIfNotFound: true);
        m_PlayerControls_SelectSlot7 = m_PlayerControls.FindAction("SelectSlot7", throwIfNotFound: true);
        m_PlayerControls_SelectSlot8 = m_PlayerControls.FindAction("SelectSlot8", throwIfNotFound: true);
        m_PlayerControls_SelectSlot9 = m_PlayerControls.FindAction("SelectSlot9", throwIfNotFound: true);
        m_PlayerControls_SelectSlot10 = m_PlayerControls.FindAction("SelectSlot10", throwIfNotFound: true);
        m_PlayerControls_Action = m_PlayerControls.FindAction("Action", throwIfNotFound: true);
        m_PlayerControls_DrawWeapon = m_PlayerControls.FindAction("DrawWeapon", throwIfNotFound: true);
        m_PlayerControls_SheathWeapon = m_PlayerControls.FindAction("SheathWeapon", throwIfNotFound: true);
        m_PlayerControls_Dash = m_PlayerControls.FindAction("Dash", throwIfNotFound: true);
        m_PlayerControls_Crafting = m_PlayerControls.FindAction("Crafting", throwIfNotFound: true);
        m_PlayerControls_Building = m_PlayerControls.FindAction("Building", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerControls
    private readonly InputActionMap m_PlayerControls;
    private List<IPlayerControlsActions> m_PlayerControlsActionsCallbackInterfaces = new List<IPlayerControlsActions>();
    private readonly InputAction m_PlayerControls_Move;
    private readonly InputAction m_PlayerControls_Inventory;
    private readonly InputAction m_PlayerControls_Pause;
    private readonly InputAction m_PlayerControls_Interaction;
    private readonly InputAction m_PlayerControls_SelectSlot1;
    private readonly InputAction m_PlayerControls_SelectSlot2;
    private readonly InputAction m_PlayerControls_SelectSlot3;
    private readonly InputAction m_PlayerControls_SelectSlot4;
    private readonly InputAction m_PlayerControls_SelectSlot5;
    private readonly InputAction m_PlayerControls_SelectSlot6;
    private readonly InputAction m_PlayerControls_SelectSlot7;
    private readonly InputAction m_PlayerControls_SelectSlot8;
    private readonly InputAction m_PlayerControls_SelectSlot9;
    private readonly InputAction m_PlayerControls_SelectSlot10;
    private readonly InputAction m_PlayerControls_Action;
    private readonly InputAction m_PlayerControls_DrawWeapon;
    private readonly InputAction m_PlayerControls_SheathWeapon;
    private readonly InputAction m_PlayerControls_Dash;
    private readonly InputAction m_PlayerControls_Crafting;
    private readonly InputAction m_PlayerControls_Building;
    public struct PlayerControlsActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerControlsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerControls_Move;
        public InputAction @Inventory => m_Wrapper.m_PlayerControls_Inventory;
        public InputAction @Pause => m_Wrapper.m_PlayerControls_Pause;
        public InputAction @Interaction => m_Wrapper.m_PlayerControls_Interaction;
        public InputAction @SelectSlot1 => m_Wrapper.m_PlayerControls_SelectSlot1;
        public InputAction @SelectSlot2 => m_Wrapper.m_PlayerControls_SelectSlot2;
        public InputAction @SelectSlot3 => m_Wrapper.m_PlayerControls_SelectSlot3;
        public InputAction @SelectSlot4 => m_Wrapper.m_PlayerControls_SelectSlot4;
        public InputAction @SelectSlot5 => m_Wrapper.m_PlayerControls_SelectSlot5;
        public InputAction @SelectSlot6 => m_Wrapper.m_PlayerControls_SelectSlot6;
        public InputAction @SelectSlot7 => m_Wrapper.m_PlayerControls_SelectSlot7;
        public InputAction @SelectSlot8 => m_Wrapper.m_PlayerControls_SelectSlot8;
        public InputAction @SelectSlot9 => m_Wrapper.m_PlayerControls_SelectSlot9;
        public InputAction @SelectSlot10 => m_Wrapper.m_PlayerControls_SelectSlot10;
        public InputAction @Action => m_Wrapper.m_PlayerControls_Action;
        public InputAction @DrawWeapon => m_Wrapper.m_PlayerControls_DrawWeapon;
        public InputAction @SheathWeapon => m_Wrapper.m_PlayerControls_SheathWeapon;
        public InputAction @Dash => m_Wrapper.m_PlayerControls_Dash;
        public InputAction @Crafting => m_Wrapper.m_PlayerControls_Crafting;
        public InputAction @Building => m_Wrapper.m_PlayerControls_Building;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerControlsActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerControlsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerControlsActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Inventory.started += instance.OnInventory;
            @Inventory.performed += instance.OnInventory;
            @Inventory.canceled += instance.OnInventory;
            @Pause.started += instance.OnPause;
            @Pause.performed += instance.OnPause;
            @Pause.canceled += instance.OnPause;
            @Interaction.started += instance.OnInteraction;
            @Interaction.performed += instance.OnInteraction;
            @Interaction.canceled += instance.OnInteraction;
            @SelectSlot1.started += instance.OnSelectSlot1;
            @SelectSlot1.performed += instance.OnSelectSlot1;
            @SelectSlot1.canceled += instance.OnSelectSlot1;
            @SelectSlot2.started += instance.OnSelectSlot2;
            @SelectSlot2.performed += instance.OnSelectSlot2;
            @SelectSlot2.canceled += instance.OnSelectSlot2;
            @SelectSlot3.started += instance.OnSelectSlot3;
            @SelectSlot3.performed += instance.OnSelectSlot3;
            @SelectSlot3.canceled += instance.OnSelectSlot3;
            @SelectSlot4.started += instance.OnSelectSlot4;
            @SelectSlot4.performed += instance.OnSelectSlot4;
            @SelectSlot4.canceled += instance.OnSelectSlot4;
            @SelectSlot5.started += instance.OnSelectSlot5;
            @SelectSlot5.performed += instance.OnSelectSlot5;
            @SelectSlot5.canceled += instance.OnSelectSlot5;
            @SelectSlot6.started += instance.OnSelectSlot6;
            @SelectSlot6.performed += instance.OnSelectSlot6;
            @SelectSlot6.canceled += instance.OnSelectSlot6;
            @SelectSlot7.started += instance.OnSelectSlot7;
            @SelectSlot7.performed += instance.OnSelectSlot7;
            @SelectSlot7.canceled += instance.OnSelectSlot7;
            @SelectSlot8.started += instance.OnSelectSlot8;
            @SelectSlot8.performed += instance.OnSelectSlot8;
            @SelectSlot8.canceled += instance.OnSelectSlot8;
            @SelectSlot9.started += instance.OnSelectSlot9;
            @SelectSlot9.performed += instance.OnSelectSlot9;
            @SelectSlot9.canceled += instance.OnSelectSlot9;
            @SelectSlot10.started += instance.OnSelectSlot10;
            @SelectSlot10.performed += instance.OnSelectSlot10;
            @SelectSlot10.canceled += instance.OnSelectSlot10;
            @Action.started += instance.OnAction;
            @Action.performed += instance.OnAction;
            @Action.canceled += instance.OnAction;
            @DrawWeapon.started += instance.OnDrawWeapon;
            @DrawWeapon.performed += instance.OnDrawWeapon;
            @DrawWeapon.canceled += instance.OnDrawWeapon;
            @SheathWeapon.started += instance.OnSheathWeapon;
            @SheathWeapon.performed += instance.OnSheathWeapon;
            @SheathWeapon.canceled += instance.OnSheathWeapon;
            @Dash.started += instance.OnDash;
            @Dash.performed += instance.OnDash;
            @Dash.canceled += instance.OnDash;
            @Crafting.started += instance.OnCrafting;
            @Crafting.performed += instance.OnCrafting;
            @Crafting.canceled += instance.OnCrafting;
            @Building.started += instance.OnBuilding;
            @Building.performed += instance.OnBuilding;
            @Building.canceled += instance.OnBuilding;
        }

        private void UnregisterCallbacks(IPlayerControlsActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Inventory.started -= instance.OnInventory;
            @Inventory.performed -= instance.OnInventory;
            @Inventory.canceled -= instance.OnInventory;
            @Pause.started -= instance.OnPause;
            @Pause.performed -= instance.OnPause;
            @Pause.canceled -= instance.OnPause;
            @Interaction.started -= instance.OnInteraction;
            @Interaction.performed -= instance.OnInteraction;
            @Interaction.canceled -= instance.OnInteraction;
            @SelectSlot1.started -= instance.OnSelectSlot1;
            @SelectSlot1.performed -= instance.OnSelectSlot1;
            @SelectSlot1.canceled -= instance.OnSelectSlot1;
            @SelectSlot2.started -= instance.OnSelectSlot2;
            @SelectSlot2.performed -= instance.OnSelectSlot2;
            @SelectSlot2.canceled -= instance.OnSelectSlot2;
            @SelectSlot3.started -= instance.OnSelectSlot3;
            @SelectSlot3.performed -= instance.OnSelectSlot3;
            @SelectSlot3.canceled -= instance.OnSelectSlot3;
            @SelectSlot4.started -= instance.OnSelectSlot4;
            @SelectSlot4.performed -= instance.OnSelectSlot4;
            @SelectSlot4.canceled -= instance.OnSelectSlot4;
            @SelectSlot5.started -= instance.OnSelectSlot5;
            @SelectSlot5.performed -= instance.OnSelectSlot5;
            @SelectSlot5.canceled -= instance.OnSelectSlot5;
            @SelectSlot6.started -= instance.OnSelectSlot6;
            @SelectSlot6.performed -= instance.OnSelectSlot6;
            @SelectSlot6.canceled -= instance.OnSelectSlot6;
            @SelectSlot7.started -= instance.OnSelectSlot7;
            @SelectSlot7.performed -= instance.OnSelectSlot7;
            @SelectSlot7.canceled -= instance.OnSelectSlot7;
            @SelectSlot8.started -= instance.OnSelectSlot8;
            @SelectSlot8.performed -= instance.OnSelectSlot8;
            @SelectSlot8.canceled -= instance.OnSelectSlot8;
            @SelectSlot9.started -= instance.OnSelectSlot9;
            @SelectSlot9.performed -= instance.OnSelectSlot9;
            @SelectSlot9.canceled -= instance.OnSelectSlot9;
            @SelectSlot10.started -= instance.OnSelectSlot10;
            @SelectSlot10.performed -= instance.OnSelectSlot10;
            @SelectSlot10.canceled -= instance.OnSelectSlot10;
            @Action.started -= instance.OnAction;
            @Action.performed -= instance.OnAction;
            @Action.canceled -= instance.OnAction;
            @DrawWeapon.started -= instance.OnDrawWeapon;
            @DrawWeapon.performed -= instance.OnDrawWeapon;
            @DrawWeapon.canceled -= instance.OnDrawWeapon;
            @SheathWeapon.started -= instance.OnSheathWeapon;
            @SheathWeapon.performed -= instance.OnSheathWeapon;
            @SheathWeapon.canceled -= instance.OnSheathWeapon;
            @Dash.started -= instance.OnDash;
            @Dash.performed -= instance.OnDash;
            @Dash.canceled -= instance.OnDash;
            @Crafting.started -= instance.OnCrafting;
            @Crafting.performed -= instance.OnCrafting;
            @Crafting.canceled -= instance.OnCrafting;
            @Building.started -= instance.OnBuilding;
            @Building.performed -= instance.OnBuilding;
            @Building.canceled -= instance.OnBuilding;
        }

        public void RemoveCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerControlsActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerControlsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerControlsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);
    public interface IPlayerControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
        void OnSelectSlot1(InputAction.CallbackContext context);
        void OnSelectSlot2(InputAction.CallbackContext context);
        void OnSelectSlot3(InputAction.CallbackContext context);
        void OnSelectSlot4(InputAction.CallbackContext context);
        void OnSelectSlot5(InputAction.CallbackContext context);
        void OnSelectSlot6(InputAction.CallbackContext context);
        void OnSelectSlot7(InputAction.CallbackContext context);
        void OnSelectSlot8(InputAction.CallbackContext context);
        void OnSelectSlot9(InputAction.CallbackContext context);
        void OnSelectSlot10(InputAction.CallbackContext context);
        void OnAction(InputAction.CallbackContext context);
        void OnDrawWeapon(InputAction.CallbackContext context);
        void OnSheathWeapon(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnCrafting(InputAction.CallbackContext context);
        void OnBuilding(InputAction.CallbackContext context);
    }
}
