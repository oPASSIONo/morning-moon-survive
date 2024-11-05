using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

[CreateAssetMenu]
public class AnimalEggSO : ItemSO, IDestroyableItem, IItemAction
{
    public string ActionName { get; }
    public AudioClip actionSFX { get; }
    public bool PerformAction(GameObject character, List<ItemParameter> itemState, int amount)
    {
        throw new System.NotImplementedException();
    }
    [field: SerializeField] public AnimalSO Animal { get; set; }
}
