using System.Collections;
using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float fastForwardDuration = 5f; // Duration to fast forward in seconds

    public void Interact()
    {
        if (TimeManager.Instance != null && TimeManager.Instance.IsNightTime())
        {
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
        float targetTimeOfDay = TimeManager.Instance.DayStartTime / 24f;

        while (TimeManager.Instance.CurrentTimeOfDay < targetTimeOfDay || TimeManager.Instance.CurrentTimeOfDay > 0.75f)
        {
            TimeManager.Instance.UpdateTime();
            yield return null;
        }

        TimeManager.Instance.StopFastForward();
        TimeManager.Instance.CurrentTimeOfDay = targetTimeOfDay;

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