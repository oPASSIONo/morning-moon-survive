using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
public class SceneTransitionTrigger : MonoBehaviour
{
    [SerializeField] private string targetScene;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has the "Player" tag and is a NetworkObject
        if (other.CompareTag("Player") && other.TryGetComponent(out NetworkObject networkObject))
        {
            // Ensure it's the local player
            if (networkObject.IsOwner)
            {
                GameManager.Instance.LoadScene(targetScene);
            }
        }
        /*if (other.CompareTag("Player"))
        {
            GameManager.Instance.LoadScene(targetScene);
        }*/
    }
 
}