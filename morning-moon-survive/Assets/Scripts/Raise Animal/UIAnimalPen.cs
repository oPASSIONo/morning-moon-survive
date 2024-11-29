using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimalPen : MonoBehaviour,IInteractable
{
    [SerializeField] private GameObject animalPenUI;
    // StaIInteractablert is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        animalPenUI.SetActive(true);
    }

    public void ShowInteractPrompt()
    {
        
    }

    public void HideInteractPrompt()
    {
        
    }
}
