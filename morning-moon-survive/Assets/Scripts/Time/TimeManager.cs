using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [Tooltip("Length of a full day in real-world minutes.")]
    private float dayLengthInMinutes = 24f;

    [Tooltip("Start time of the day in hours (e.g., 6 = 6:00 AM).")]
    public float dayStartTime { get; private set; } = 6f;

    [Tooltip("Start time of the night in hours (e.g., 18 = 6:00 PM).")]
    public float nightStartTime { get; private set; } = 18f;

    [Tooltip("The current time of day in the game. 0 = Midnight, 0.5 = Noon, 1 = Next Midnight.")]
    [Range(0f, 1f)]
    public float currentTimeOfDay = 0f;

    [Tooltip("The speed at which time passes.")]
    private float timeMultiplier;

    [Tooltip("Multiplier for fast-forwarding time.")]
    private float fastForwardMultiplier = 100f;

    [Tooltip("Event triggered when the day starts.")]
    public UnityEngine.Events.UnityEvent OnDayStart;

    [Tooltip("Event triggered when the night starts.")]
    public UnityEngine.Events.UnityEvent OnNightStart;

    private bool isDay = true;

    [Tooltip("TextMeshPro UI component for displaying the time.")]
    public TextMeshProUGUI timeText;

    [Tooltip("TextMeshPro UI component for displaying the day count.")]
    public TextMeshProUGUI dayCountText;

    public int dayCount { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Start the game during the day
        float dayStartInMinutes = dayStartTime * 60f;
        currentTimeOfDay = dayStartInMinutes / (24f * 60f);
    }

    private void Start()
    {
        timeMultiplier = 1f / (dayLengthInMinutes * 60f);
    }

    private void Update()
    {
        UpdateTime();
        UpdateTimeDisplay();
    }

    public void UpdateTime()
    {
        float previousTimeOfDay = currentTimeOfDay;

        currentTimeOfDay += Time.deltaTime * timeMultiplier;
        currentTimeOfDay %= 1f;

        float currentHour = currentTimeOfDay * 24f;

        if (previousTimeOfDay > currentTimeOfDay)
        {
            // It has transitioned from the end of one day to the start of the next
            dayCount++;
            Debug.Log("Day Started: Day Count = " + dayCount);
        }

        if (isDay && currentHour >= nightStartTime)
        {
            isDay = false;
            OnNightStart.Invoke();
            Debug.Log("Night Started");
        }
        else if (!isDay && currentHour < dayStartTime)
        {
            isDay = true;
            OnDayStart.Invoke();
            Debug.Log("Day Started");
        }
    }

    private void UpdateTimeDisplay()
    {
        float totalMinutes = currentTimeOfDay * 24f * 60f;
        int hours = Mathf.FloorToInt(totalMinutes / 60f);
        int minutes = Mathf.FloorToInt(totalMinutes % 60f);
        string timeString = string.Format("{0:00}:{1:00}", hours, minutes);
        timeText.text = timeString;

        // Update the day count display
        dayCountText.text = "Day: " + dayCount;

        // Debug log to show the current in-game time
    }

    public bool IsNightTime()
    {
        float currentHour = currentTimeOfDay * 24f;
        return currentHour >= nightStartTime || currentHour < dayStartTime;
    }

    public void StartFastForward()
    {
        PlayerStateManager.Instance.SetState(PlayerStateManager.PlayerState.Sleep);
        timeMultiplier = fastForwardMultiplier / (dayLengthInMinutes * 60f);
    }

    public void StopFastForward()
    {
        PlayerStateManager.Instance.SetState(PlayerStateManager.PlayerState.Normal);
        timeMultiplier = 1f / (dayLengthInMinutes * 60f);
    }
}
