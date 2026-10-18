using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{
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
        IsPm = false;
        _hours = 6;
        FractionsHour = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopedTime)
        {
            FractionsHour += SpeedTime * Time.deltaTime;
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
    }
}