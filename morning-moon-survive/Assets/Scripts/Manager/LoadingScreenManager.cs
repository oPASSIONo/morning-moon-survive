using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScreenManager : MonoBehaviour
{
    public TMP_Text loadingText;
    public TMP_Text promptText;

    private string targetSceneName;

    private void Start()
    {
        promptText.gameObject.SetActive(false);
        loadingText.text = "Loading...";
    }

    public void LoadScene(string sceneName)
    {
        targetSceneName = sceneName;
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        // Load the target scene asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(targetSceneName);
        asyncOperation.allowSceneActivation = false;

        // Wait until the scene is fully loaded
        while (!asyncOperation.isDone)
        {
            // Check if the scene is ready to activate
            if (asyncOperation.progress >= 0.9f)
            {
                // Show the prompt to continue
                loadingText.gameObject.SetActive(false);
                promptText.gameObject.SetActive(true);

                // Wait for any key press
                if (Input.anyKeyDown)
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}