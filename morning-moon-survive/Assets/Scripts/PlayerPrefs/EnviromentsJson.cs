using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnviromentsJson
{
    [JsonProperty] public float Time;
    [JsonProperty] public int Day;

    public void SetTime(TimeManager present)
    {
        Time = present.CurrentTimeOfDay;
        Day = present.DayCount;
    }
}
