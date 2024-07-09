using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CombatSystem
{

    public class DamageDealer : MonoBehaviour
    {
        //private bool canDealDamage;
        //private List<GameObject> hasDealDamage;
        private HashSet<GameObject> hasDealtDamage;


        [SerializeField] private float weaponLength = 1.5f;
        [SerializeField] private float weaponDamage = 10f;
        [SerializeField] private LayerMask targetLayerMask;

        private void Start()
        {
            //canDealDamage = false;
            //hasDealDamage = new List<GameObject>();
            hasDealtDamage = new HashSet<GameObject>();

        }

        private void Update()
        {
            /*if (canDealDamage)
            {
                RaycastHit hit;

                int layerMask = 1 << 9;
                if (Physics.Raycast(transform.position, -transform.up, out hit , weaponLength, layerMask))
                {
                    if (hit.transform.TryGetComponent(out Tree tree) &&!hasDealDamage.Contains(hit.transform.gameObject))
                    {
                        Debug.Log("DAMAGE");
                        tree.TakeDamage(weaponDamage);
                        hasDealDamage.Add(hit.transform.gameObject);
                    }
                }
            }*/
            /*if (canDealDamage)
            {
                PerformRaycast();
            }*/
        }

        public void PerformAttack()
        {
            hasDealtDamage.Clear();  // Clear previous attack records

            RaycastHit hit;

            // Calculate raycast direction based on player's facing direction (or any desired direction)
            Vector3 raycastDirection = transform.forward;

            // Adjust raycast distance based on weapon length or interaction range
            float maxDistance = weaponLength; // Adjust this based on your game's mechanics
        
            // Raycast from the player's position in the calculated direction
            if (Physics.Raycast(transform.position, raycastDirection, out hit, maxDistance, targetLayerMask))
            {
                var damageable = hit.transform.GetComponent<IDamageable>();
                if (damageable != null && !hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    Debug.Log("DAMAGE");
                    damageable.TakeDamage(weaponDamage);
                    hasDealtDamage.Add(hit.transform.gameObject);
                }
            }
            else
            {
                Debug.Log("No hit detected.");
            }

            // Debug visualization
            Debug.DrawRay(transform.position, raycastDirection * maxDistance, Color.red, 0.5f);
        }


        /*public void StartDealDamage()
        {
            canDealDamage = true;
            hasDealDamage.Clear();
        }

        public void EndDealDamage()
        {
            canDealDamage = false;
        }*/

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * weaponLength);
        }
    }
}
