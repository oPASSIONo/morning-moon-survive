using System.Collections;
using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    private float fastForwardDuration = 5f; // Duration to fast forward in seconds

    public void Interact()
    {
        if (TimeManager.Instance != null && TimeManager.Instance.IsNightTime())
        {
            // Start fast-forwarding time
            TimeManager.Instance.StartFastForward();
            StartCoroutine(FastForwardToDay());
            Debug.Log("Player sleeping to skip to morning.");
        }
        else
        {
            Debug.Log("You can only sleep at night.");
        }
    }
    private IEnumerator FastForwardToDay()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fastForwardDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Stop fast-forwarding time and set to start of the day
        TimeManager.Instance.StopFastForward();
        TimeManager.Instance.currentTimeOfDay = TimeManager.Instance.dayStartTime / 24f;
    }
    public void ShowInteractPrompt()
    {
        // Implement UI or prompt to indicate interaction (e.g., display "Press E to interact")
    }

    public void HideInteractPrompt()
    {
        // Implement UI or prompt to indicate interaction (e.g., hide the interact prompt)
    }
}