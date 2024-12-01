using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Task = System.Threading.Tasks.Task;
using Unity.Netcode;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private GameObject _loaderCanvas;
    //public void SetLoaderCanvas(bool isActive) => _loaderCanvas.SetActive(isActive);
    
    [SerializeField] private TMP_Text loadingText;
    private CanvasGroup _canvasGroup;
    private float fadeDuration = 0.5f; // Duration of the fade-out effect in seconds
    private float delayBeforeFadeOut = 3f; // Delay before starting the fade-out

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
        /*_loaderCanvas.SetActive(true);
        _canvasGroup.alpha = 1f; // Ensure the canvas starts fully visible
        SceneManager.LoadScene(sceneName);
        OnLoadComplete?.Invoke();
        FadeOutLoading();*/
        
        // Ensure loader canvas is visible
        void ActivateLoader()
        {
            _loaderCanvas.SetActive(true);
            _canvasGroup.alpha = 1f; // Ensure the canvas starts fully visible
        }

        // Fallback to standard scene loading if NetworkManager or SceneManager is not initialized
        if (NetworkManager.Singleton == null || NetworkManager.Singleton.SceneManager == null)
        {
            Debug.Log("NetworkManager or SceneManager is not initialized. Falling back to standard scene loading.");
            ActivateLoader();
            SceneManager.LoadScene(sceneName); // Use Unity's default SceneManager
            OnLoadComplete?.Invoke();
            FadeOutLoading();
            return;
        }

        // Ensure the loader canvas is activated
        ActivateLoader();

        // Network-based scene loading
        if (NetworkManager.Singleton.IsHost)
        {
            Debug.Log($"Host is loading scene: {sceneName}");
            NetworkManager.Singleton.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        else if (NetworkManager.Singleton.IsClient)
        {
            Debug.Log($"Client is attempting to load scene: {sceneName}");
            NetworkManager.Singleton.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        else
        {
            Debug.LogWarning("Neither host nor client role detected. Falling back to standard scene loading.");
            SceneManager.LoadScene(sceneName); // Fallback to standard Unity SceneManager
        }

        // Invoke completion logic
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
