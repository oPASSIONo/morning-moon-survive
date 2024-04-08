using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmountController : MonoBehaviour
{
    private TMP_InputField inputField;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the InputField component
        inputField = GetComponent<TMP_InputField>();

        // Set the content type to Integer Number
        inputField.contentType = TMP_InputField.ContentType.IntegerNumber;

        // Add a listener for the value changed event
        inputField.onValueChanged.AddListener(ValidateInput);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ValidateInput(string value)
    {
        // Remove any non-integer characters from the input
        string filteredValue = string.Empty;
        foreach (char c in value)
        {
            if (char.IsDigit(c))
            {
                filteredValue += c;
            }
        }

        // Update the input field text with the filtered value
        inputField.text = filteredValue;
    }

    /*public int InputToAmount()
    {
        int.TryParse(inputField.text, out int amount);
        return int amount;
    }*/
}
