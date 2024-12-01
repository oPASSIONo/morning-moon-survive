using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;
using Unity.Netcode;

public class Item : NetworkBehaviour
{
    [field: SerializeField] public ItemSO InventoryItem { get; private set; }
    [field: SerializeField] public int Quantity { get; set; } = 1;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float duration = 0.3f;
    
    /*
    public void DestroyItem()
    {
        GetComponent<Collider>().enabled = false;
        //StartCoroutine(AnimateItemPickup());
        Destroy(gameObject);
    }
    */
    public void DestroyItem()
    {
        if (IsServer)
        {
            DestroyItemClientRPC();
            NetworkObject.Despawn();
        }
        else
        {
            Debug.LogWarning("DestroyItem() should only be called on the server.");
        }
    }

    [ClientRpc]
    public void DestroyItemClientRPC()
    {
        // Optional: Play audio on clients before item is destroyed
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Destroy the item immediately
        Destroy(gameObject);
    }

    [ClientRpc]
    public void UpdateQuantityClientRPC(int newQuantity)
    {
        Quantity = newQuantity;
    }

    private IEnumerator AnimateItemPickup()
    {
        audioSource.Play();
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