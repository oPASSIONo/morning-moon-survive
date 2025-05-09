using UnityEngine;
using UnityEngine.Events;

public class EnemyPartHitDetector : MonoBehaviour
{
    public UnityEvent<Collider> OnPlayerHit; // Unity event to notify when the player is hit

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            OnPlayerHit?.Invoke(other);
        }
    }
    
}