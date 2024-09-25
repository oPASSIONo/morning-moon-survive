using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class ItemJson
{
    public string Type, Name;
    public int Amount;

    private List<ItemJson> itemSlots;

    public ItemJson(string type, string name, int amount)
    {
        this.Type = type;
        this.Name = name;
        this.Amount = amount;
        
        itemSlots = new List<ItemJson>();
    }
}
