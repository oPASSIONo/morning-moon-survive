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

    public event Action OnLoadComplete;

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
    }

    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        _loaderCanvas.SetActive(true);
        do
        {
            await Task.Delay(100);
        } while (scene.progress<0.9f);

        await Task.Delay(1000);
        
        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);
        OnLoadComplete?.Invoke();
    }
}
