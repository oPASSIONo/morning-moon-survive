using UnityEngine;

public class SceneTransitionTrigger : MonoBehaviour
{
    [SerializeField] private string targetScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //SceneTransitionManager.Instance.TransitionToScene(targetScene);
            GameManager.Instance.LoadScene(targetScene);
        }
    }
}