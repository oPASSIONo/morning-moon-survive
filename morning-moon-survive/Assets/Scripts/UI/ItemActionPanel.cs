using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class ItemActionPanel : MonoBehaviour
    {
        [SerializeField] public Button dropBtn;
        //[SerializeField] private GameObject buttonPrefab;

        /*public void AddButton(string name,Action onClickAction)
        {
            GameObject button = Instantiate(buttonPrefab, transform);
            button.GetComponent<Button>().onClick.AddListener(()=>onClickAction());
            button.GetComponentInChildren<TMPro.TMP_Text>().text = name;
        }*/
        public void Toggle(bool val)
        {
            /*if (val)
            {
                RemoveOldButton();
            }*/
            gameObject.SetActive(val);
        }

        /*public void RemoveOldButton()
        {
            foreach (Transform transformChildObjects in transform)
            {
                Destroy(transformChildObjects.gameObject);
            }
        }*/
    }
}

