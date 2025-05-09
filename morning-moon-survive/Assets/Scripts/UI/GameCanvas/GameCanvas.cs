using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas Instance { get; private set; }
    public UIHealthBar healthBar;
    public UIStaminaBar staminaBar;
    public UISatietyBar satietyBar;
    public CraftButtonHandler craftButtonHandler;
    public UICraftingPage craftingPage;
    public GameObject notiBox;

    [SerializeField] private AnimalPenView animalPenView;

    public AnimalPenView GetAnimalPenView()
    {
        return animalPenView;
    }
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
        }
    }
}
