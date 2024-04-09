using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTools : MonoBehaviour
{
    [Header("General")] public List<ItemCategory> itemList;

    public int selectedItem;

    [Space(20)] [Header("Item gameobjects")] 
    [SerializeField] private GameObject pickAxe;

    private Dictionary<ItemCategory, GameObject> itemSetActive = new Dictionary<ItemCategory, GameObject>() { };
    
    private PlayerInput playerInput;
    
    
    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.PlayerControls.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        itemSetActive.Add(ItemCategory.Equipment,pickAxe);
        
        //NewItemSelected();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NewItemSelected()
    {
        //object.SetActive(false);

        GameObject selectedItemGameobject = itemSetActive[itemList[selectedItem]];
        selectedItemGameobject.SetActive(true);
    }
    
    private void OnEnable()
    {
        // Subscribe to hotbar selection actions
        playerInput.FindAction("SelectSlot1").performed += ctx => SelectSlot(1);

    }
    
    private void OnDisable()
    {
        // Unsubscribe from hotbar selection actions
        playerInput.FindAction("SelectSlot1").performed -= ctx => SelectSlot(1);
    }
    
    private void SelectSlot(int slot)
    {
        Debug.Log("Selected slot " + slot);
        selectedItem = slot-1;
        NewItemSelected();
    }
}
