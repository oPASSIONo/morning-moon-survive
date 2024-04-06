using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class Loader
{
    public enum Scene
    {
        
    }
    public static int targetSceneIndex;

    public static void Load(string targetSceneName)
    {
        SceneManager.LoadScene(targetSceneName);
    }

}
