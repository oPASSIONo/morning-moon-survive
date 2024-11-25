using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;

public class TimeManager : NetworkBehaviour
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

    /*[Tooltip("The current time of day in the game. 0 = Midnight, 0.5 = Noon, 1 = Next Midnight.")]
    [SerializeField, Range(0f, 1f)]
    private float currentTimeOfDay = 0f;*/

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

    private PlayerStateManager playerStateManager;

    
    // NetworkVariables to sync time and day count
    public NetworkVariable<float> currentTimeOfDay = new NetworkVariable<float>(0f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> dayCount = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public float DayStartTime => dayStartTime;
    public float NightStartTime => nightStartTime;
    public bool IsStartTimer { get; private set; }
    public void SetStartTimer(bool isStart) => IsStartTimer = isStart;
    public void SetDayCount(int value) => dayCount.Value = value;

    /*public float CurrentTimeOfDay
    {
        get => currentTimeOfDay;
        set => currentTimeOfDay = Mathf.Clamp(value, 0f, 1f);
    }*/

    //public int DayCount { get; private set; } = 0;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            DontDestroyOnLoad(gameObject);
        }

        currentTimeOfDay.Value = (dayStartTime * 60f) / (24f * 60f); // Normalize the start time

        /*float dayStartInMinutes = dayStartTime * 60f;
        currentTimeOfDay = dayStartInMinutes / (24f * 60f);*/
    }

    private void Start()
    {
        timeMultiplier = 1f / (dayLengthInMinutes * 60f);
    }

    private void OnClientConnected(ulong obj)
    {
        if (NetworkManager.Singleton.LocalClientId == obj)
        {
            TryAssignLocalPlayer();
        }
    }

    private void TryAssignLocalPlayer()
    {
        foreach (var networkObject in FindObjectsOfType<NetworkObject>())
        {
            if (networkObject.IsLocalPlayer)
            {
                playerStateManager = networkObject.GetComponent<PlayerStateManager>();
                break;
            }
        }
        
        if (playerStateManager != null)
        {
            Debug.Log("Local player's TimeManager found.");
        }
        else
        {
            Debug.Log("Local player's TimeManager not found.");
        }
    }
    private void Update()
    {
        if (IsStartTimer)
        {
            if (IsOwner)  // Only allow the host to update the time
            {
                UpdateTime();
            }
            UpdateTimeDisplay();
        }
    }

    public void UpdateTime()
    {
        float previousTimeOfDay = currentTimeOfDay.Value;

        currentTimeOfDay.Value += Time.deltaTime * timeMultiplier;
        currentTimeOfDay.Value %= 1f;

        float currentHour = currentTimeOfDay.Value * 24f;

        if (previousTimeOfDay > currentTimeOfDay.Value)
        {
            dayCount.Value++;
            Debug.Log("Day Started: Day Count = " + dayCount.Value);
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
        float totalMinutes = currentTimeOfDay.Value  * 24f * 60f;
        int hours = Mathf.FloorToInt(totalMinutes / 60f);
        int minutes = Mathf.FloorToInt(totalMinutes % 60f);
        string timeString = string.Format("{0:00}:{1:00}", hours, minutes);
        timeText.text = "Time: " + timeString;

        dayCountText.text = "Day: " + dayCount.Value;
    }

    public bool IsNightTime()
    {
        float currentHour = currentTimeOfDay.Value * 24f;
        return currentHour >= nightStartTime || currentHour < dayStartTime;
    }

    public void StartFastForward()
    {
        playerStateManager.SetState(PlayerStateManager.PlayerState.Sleep);
        timeMultiplier = fastForwardMultiplier / (dayLengthInMinutes * 60f);
    }

    public void StopFastForward()
    {
        playerStateManager.SetState(PlayerStateManager.PlayerState.Normal);
        timeMultiplier = 1f / (dayLengthInMinutes * 60f);
    }
}
