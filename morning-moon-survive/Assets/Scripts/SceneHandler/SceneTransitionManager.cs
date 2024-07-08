using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void TransitionToScene(string sceneName)
    {
        SinglePlayModeGameController.Instance.LoadScene(sceneName);
    }
}