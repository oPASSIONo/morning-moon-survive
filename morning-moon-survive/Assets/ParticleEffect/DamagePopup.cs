using System;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.UI;

public class DamagePopup : MonoBehaviour
{
    public static DamagePopup current;
    public GameObject prefab;

    private void Awake()
    {
        current = this;
    }
    
    public void CreatePopup(Vector3 position, string damagePopup)
    {
        var popup = Instantiate(prefab, position, quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text = damagePopup;
    }
}