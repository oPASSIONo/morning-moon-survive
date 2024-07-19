using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private Collider attackCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = GetComponent<Enemy>();
        if (enemy!=null)
        {
            switch (other.tag)
            {
                case "WeakPoint":
                    break;
                case "Enemy":
                    
                    break;
            }
        }
    }

    
}
