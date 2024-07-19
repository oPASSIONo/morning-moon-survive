using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTool : MonoBehaviour
{
    
    public float Name { get; private set; }
    public float BaseDamage { get; private set; }
    public float Weight { get; private set; }
    public enum AttackType
    {
        Chop,
        Blunt,
        Pierce,
        Slash,
        Ammo
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject hitObject = other.gameObject;
        Tree tree = hitObject.GetComponent<Tree>();
        if (tree != null)
        {
            tree.TakeDamage(BaseDamage);
        }
    }
}
