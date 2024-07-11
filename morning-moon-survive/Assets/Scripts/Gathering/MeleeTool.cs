using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTool : MonoBehaviour
{
    private float baseDamage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        GameObject hitObject = other.gameObject;
        Tree tree = hitObject.GetComponent<Tree>();
        if (tree != null)
        {
            tree.TakeDamage(baseDamage);
        }
    }
}
