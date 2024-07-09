using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
   public class Tree : MonoBehaviour, IDamageable
   {
      [SerializeField] private float health = 100f;

      //[SerializeField] private float duration = 0.3f;
      [SerializeField] private ResourceDrop[] resourceDrops;


      public void TakeDamage(float amount)
      {
         health -= amount;
         Debug.Log($"Tree took {amount} damage. Remaining health: {health}");

         if (health <= 0)
         {
            DropResources();
            Destroy(gameObject);
         }
      }

      private void DropResources()
      {
         foreach (var resource in resourceDrops)
         {
            for (int i = 0; i < resource.amount; i++)
            {
               Instantiate(resource.resourcePrefab, transform.position, Quaternion.identity);
            }
         }
      }

      [System.Serializable]
      public class ResourceDrop
      {
         public GameObject resourcePrefab;
         public int amount;
      }

      //private GameObject player;


      /*private void Start()
      {
         player = GameObject.FindWithTag("Player");
      }*/

      /*public void TakeDamage(float damageAmount)
      {
         health -= damageAmount;

         if (health <= 0)
         {
            Die();
         }
      }*/

      /*private void Die()
      {
         //Destroy(this.gameObject);
         DestroyItem();
      }*/

      /*private void DestroyItem()
      {

         GetComponent<Collider>().enabled = false;
         StartCoroutine(AnimateItemPickup());
      }*/

      /*private IEnumerator AnimateItemPickup()
      {
         Vector3 startScale = transform.localScale;
         Vector3 endScale = Vector3.zero;
         float currentTime = 0;
         while (currentTime<duration)
         {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
         }
         Destroy(gameObject);
      }*/
   }
}
