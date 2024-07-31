using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        // Check if it's currently night
        if (TimeManager.Instance != null && TimeManager.Instance.IsNightTime())
        {
            // Set the time to the start of the day
            TimeManager.Instance.currentTimeOfDay = TimeManager.Instance.dayStartTime / 24f;
            Debug.Log("Set time to new day");
            Debug.Log("Player sleep");
        }
        else
        {
            Debug.Log("You can only sleep at night.");
        }
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