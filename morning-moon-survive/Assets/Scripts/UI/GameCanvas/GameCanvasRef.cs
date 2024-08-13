using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasRef : MonoBehaviour
{
    public UIHealthBar healthBar;
    public UIStaminaBar staminaBar;
    public UISatietyBar satietyBar;
    public CraftButtonHandler craftButtonHandler;
    public UICraftingPage craftingPage;
    public GameObject notiBox;

    /*[SerializeField] private GameObject inventory;

    public void SetGameCanvasAsDefault()
    {
        inventory.SetActive(false);
        notiBox.SetActive(false);
    }*/
}
