using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [Tooltip("Length of a full day in real-world minutes.")]
    [SerializeField]
    private float dayLengthInMinutes = 24f;

    [Tooltip("Start time of the day in hours (e.g., 6 = 6:00 AM).")]
    [SerializeField]
    private float dayStartTime = 6f;

    [Tooltip("Start time of the night in hours (e.g., 18 = 6:00 PM).")]
    [SerializeField]
    private float nightStartTime = 18f;

    [Tooltip("The current time of day in the game. 0 = Midnight, 0.5 = Noon, 1 = Next Midnight.")]
    [SerializeField, Range(0f, 1f)]
    private float currentTimeOfDay = 0f;

    [Tooltip("The speed at which time passes.")]
    private float timeMultiplier;

    [Tooltip("Multiplier for fast-forwarding time.")]
    [SerializeField]
    private float fastForwardMultiplier = 100f;

    [Tooltip("Event triggered when the day starts.")]
    public UnityEngine.Events.UnityEvent OnDayStart;

    [Tooltip("Event triggered when the night starts.")]
    public UnityEngine.Events.UnityEvent OnNightStart;
    
    [Tooltip("Event triggered when the day ends.")]
    public UnityEngine.Events.UnityEvent OnDayEnd;  // New event for day end
    
    private bool isDay = true;

    [Tooltip("TextMeshPro UI component for displaying the time.")]
    [SerializeField]
    private TextMeshProUGUI timeText;

    [Tooltip("TextMeshPro UI component for displaying the day count.")]
    [SerializeField]
    private TextMeshProUGUI dayCountText;

    public float DayStartTime => dayStartTime;
    public float NightStartTime => nightStartTime;
    public bool IsStartTimer { get; private set; }
    public void SetStartTimer(bool isStart) => IsStartTimer = isStart;

    public float CurrentTimeOfDay
    {
        get => currentTimeOfDay;
        set => currentTimeOfDay = Mathf.Clamp(value, 0f, 1f);
    }

    public int DayCount { get; private set; } = 0;

    public void SetDayCount(int value) => DayCount = value;

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

        float dayStartInMinutes = dayStartTime * 60f;
        currentTimeOfDay = dayStartInMinutes / (24f * 60f);
    }
    
    private void Start()
    {
        timeMultiplier = 1f / (dayLengthInMinutes * 60f);
    }

    private void Update()
    {
        if (!IsStartTimer)
        {
            UpdateTime();
            UpdateTimeDisplay();
        }
    }

    public void UpdateTime()
    {
        float previousTimeOfDay = currentTimeOfDay;

        CurrentTimeOfDay += Time.deltaTime * timeMultiplier;
        CurrentTimeOfDay %= 1f;

        float currentHour = CurrentTimeOfDay * 24f;

        if (previousTimeOfDay > CurrentTimeOfDay)
        {
            DayCount++;
            Debug.Log("Day Started: Day Count = " + DayCount);
            OnDayEnd.Invoke();  // Invoke end of day event

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
        float totalMinutes = CurrentTimeOfDay * 24f * 60f;
        int hours = Mathf.FloorToInt(totalMinutes / 60f);
        int minutes = Mathf.FloorToInt(totalMinutes % 60f);
        string timeString = string.Format("{0:00}:{1:00}", hours, minutes);
        timeText.text = "Time: " + timeString;

        dayCountText.text = "Day: " + DayCount;
    }

    public bool IsNightTime()
    {
        float currentHour = CurrentTimeOfDay * 24f;
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
