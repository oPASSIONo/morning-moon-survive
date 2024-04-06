using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberInputController : MonoBehaviour
{
    private InputField inputField;
    [SerializeField] private Button incrementButton;
    [SerializeField] private Button decrementButton;
    private int currentValue = 0; // Declare currentValue at the class level

    
    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<InputField>();
        // Add listeners for the buttons
        incrementButton.onClick.AddListener(IncrementValue);
        decrementButton.onClick.AddListener(DecrementValue);
        
        // Limit input field to accept only integers
        inputField.contentType = InputField.ContentType.IntegerNumber;
        
        // Set the initial value of the input field
        inputField.text = currentValue.ToString();
    }

    void IncrementValue()
    {
        // Increment the current value and update the input field
        currentValue++;
        inputField.text = currentValue.ToString();
    }
    
    void DecrementValue()
    {
        // Decrement the current value (if greater than 0) and update the input field
        if (currentValue > 0)
        {
            currentValue--;
            inputField.text = currentValue.ToString();
        }
    }
}
