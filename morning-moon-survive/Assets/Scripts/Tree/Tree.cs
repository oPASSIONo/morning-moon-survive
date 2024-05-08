using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
   [SerializeField] private float health = 3;
   [SerializeField] private float duration = 0.3f;


   private GameObject player;


   private void Start()
   {
      player = GameObject.FindWithTag("Player");
   }

   public void TakeDamage(float damageAmount)
   {
      health -= damageAmount;

      if (health <= 0)
      {
         Die();
      }
   }

   private void Die()
   {
      //Destroy(this.gameObject);
      DestroyItem();
   }
   
   private void DestroyItem()
   {
  
      GetComponent<Collider>().enabled = false;
      StartCoroutine(AnimateItemPickup());
        
   }

   private IEnumerator AnimateItemPickup()
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
        
   }
}
