using System;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    public bool slowTime;
    private bool stopedTime;
    [SerializeField]
    private int _hours;
    public int Hours {
        get {
            if (IsPm)
                return 12 + _hours;
            return _hours;
        }
    }
    private float FractionsHour;
    public bool IsPm { get; private set; }
    [SerializeField]
    private float SpeedTime;
    public float DeltaTime { get; private set; }

    public string GetTime()
    {
        return _hours + ":" + (60 * FractionsHour).ToString("00") + " " + (IsPm ? "pm" : "am");
    }
    public void StopTime()
    {
        stopedTime = true;
    }
    public void StartTime()
    {
        stopedTime = false;
    }
    void Start()
    {
        slowTime = false;
        IsPm = false;
        _hours = 6;
        FractionsHour = 0.5f;
    }

    void Update()
    {
        if (!stopedTime)
        {
            DeltaTime = slowTime ? 0.01f * SpeedTime * Time.fixedDeltaTime : SpeedTime * Time.fixedDeltaTime;
            FractionsHour += DeltaTime;
            while (FractionsHour >= 1f)
            {
                FractionsHour -= 1f;
                _hours++;
                if (_hours >= 12)
                {
                    _hours = 0;
                    IsPm = !IsPm;
                }
            }
        }
        else
        {
            DeltaTime = 0;
        }
    }
}