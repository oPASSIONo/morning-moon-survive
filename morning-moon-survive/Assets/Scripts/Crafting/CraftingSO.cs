using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CraftingSO", menuName = "Crafting/Crafting System", order = 1)]
public class CraftingSO : ScriptableObject
{
    public List<Recipe> recipes = new List<Recipe>();
    
    // Optional: You can add more data related to crafting system here
}

