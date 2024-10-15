using Inventory.Model;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAnimal", menuName = "Animals/AnimalSO")]
public class AnimalSO : ScriptableObject
{
    public string speciesName;
    public GameObject babyPrefab;
    public GameObject juvenilePrefab;
    public GameObject adultPrefab;
    public int babyToJuvenileTime = 3; // Days from baby to juvenile
    public int juvenileToAdultTime = 5; // Days from juvenile to adult

    // New fields for item drops
    //public bool canDropItems;           // Whether this animal can drop items (e.g., chickens drop eggs)
    public ItemSO dropItem;             // The type of item it drops (e.g., eggs)
    public int daysBetweenDrops = 2;    // Regular interval for item drops after adulthood
    public int daysAfterAdultBeforeFirstDrop = 1;  // Delay for first drop after becoming an adult
}
