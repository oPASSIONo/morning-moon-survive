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
        float targetTimeOfDay = TimeManager.Instance.dayStartTime / 24f;

        while (TimeManager.Instance.currentTimeOfDay < targetTimeOfDay || TimeManager.Instance.currentTimeOfDay > 0.75f)
        {
            TimeManager.Instance.UpdateTime();
            yield return null;
        }

        // Stop fast-forwarding time
        TimeManager.Instance.StopFastForward();
        TimeManager.Instance.currentTimeOfDay = targetTimeOfDay;

        // Ensure the day count updates correctly if the fast forward passes midnight
        TimeManager.Instance.UpdateTime();
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