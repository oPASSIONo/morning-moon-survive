using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using Task = System.Threading.Tasks.Task;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private GameObject _loaderCanvas;
    
    [SerializeField] private TMP_Text loadingText;
    private CanvasGroup _canvasGroup;
    private float fadeDuration = 0.5f; // Duration of the fade-out effect in seconds
    private float delayBeforeFadeOut = 1f; // Delay before starting the fade-out

    public event Action OnLoadComplete;
    public event Action OnLoaderFadeOut;
    
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _canvasGroup = _loaderCanvas.GetComponent<CanvasGroup>();
    }
    public void LoadScene(string sceneName)
    {
        _loaderCanvas.SetActive(true);
        _canvasGroup.alpha = 1f; // Ensure the canvas starts fully visible

        // Load the scene normally (synchronously)
        SceneManager.LoadScene(sceneName);
        // Once the scene is loaded, fade out the loading screen
        OnLoadComplete?.Invoke();
        FadeOutLoading();

    }

    public void PortalWarp()
    {
        _loaderCanvas.SetActive(true);
        _canvasGroup.alpha = 1f; // Ensure the canvas starts fully visible
        OnLoadComplete?.Invoke();
        FadeOutLoading();
    }
    /*public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        _loaderCanvas.SetActive(true);
        _canvasGroup.alpha = 1f; // Ensure the canvas starts fully visible
        do
        {
            await Task.Delay(100);
        } while (scene.progress<0.9f);

        await Task.Delay(1000);
        
        scene.allowSceneActivation = true;
        
        OnLoadComplete?.Invoke();
        FadeOutLoading();

    }*/
    private IEnumerator FadeOutCanvasGroup()
    {
        OnLoaderFadeOut?.Invoke();
        float startAlpha = _canvasGroup.alpha;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, time / fadeDuration);
            yield return null;
        }

        _canvasGroup.alpha = 0;
        _loaderCanvas.SetActive(false);
    }
    private IEnumerator WaitAndFadeOut()
    {
        // Wait for the specified delay before starting the fade-out
        yield return new WaitForSeconds(delayBeforeFadeOut);
        
        // Start fading out the canvas group
        StartCoroutine(FadeOutCanvasGroup());
    }
    public void FadeOutLoading()
    {
        
        // Fade out the canvas group
        StartCoroutine(WaitAndFadeOut());
    }
}
