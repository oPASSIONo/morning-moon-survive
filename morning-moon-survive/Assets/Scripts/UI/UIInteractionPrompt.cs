// UIInteractionPrompt.cs
using UnityEngine;

public class UIInteractionPrompt : MonoBehaviour
{
    public GameObject promptObject;

    public void Show()
    {
        promptObject.SetActive(true);
    }

    public void Hide()
    {
        promptObject.SetActive(false);
    }
}