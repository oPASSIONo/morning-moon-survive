using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    public int damageAmount = 10; // Amount of damage the pickaxe deals

    /*void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            // If the pickaxe hits a tree, apply damage to the tree
            Tree tree = other.GetComponent<Tree>();
            if (tree != null)
            {
                tree.TakeDamage(damageAmount);
            }
        }
    }*/
}
