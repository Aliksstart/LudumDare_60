using System;
using UnityEngine;

public class GenerationView
{
    private int idAgr = 0;
    private float stepButton = 0.05f;

    // ----
    private float kPower = 0.015f;
    private float kRPM = 3f;
    // ----
    public bool IsConnectedGrid { get; private set; }
    public Action<float, float, bool, int> UpdateConnetedGrid;
    public float CommissioningHydroUnit{ get; private set ;}
    public Action<float, int> UpdateCommissioningHydroUnit;
    public void Commissionin() {
        float x = CommissioningHydroUnit + stepButton;
        CommissioningHydroUnit = x > 1.0f ? 1.0f : x;
        UpdateCommissioningHydroUnit(CommissioningHydroUnit, idAgr);
    }
    public void Shutdown() {
        float x = CommissioningHydroUnit - stepButton;
        CommissioningHydroUnit = x < 0.0f ? 0.0f : x;
        UpdateCommissioningHydroUnit(CommissioningHydroUnit, idAgr);
    }
    private float MaxPower = 400;
    public float CurrentPower { get; private set; }
    public Action<float, float, int> UpdatePower;
    private float MaxRPM = 1.3f;
    public float CurrentRPM { get; private set; }
    public Action<float, int> UpdateRPM;
    public GenerationView(int id)
    {
        IsConnectedGrid = false;
        idAgr = id;
    }
    public void ConnectionGen() {
        IsConnectedGrid = true;
        float RPM = CurrentRPM;
        Update();
        UpdateConnetedGrid(RPM, CurrentPower, IsConnectedGrid, idAgr);
    }
    public void DisconnectionGen() {
        IsConnectedGrid = false;
        float Power = CurrentPower;
        Update();
        UpdateConnetedGrid(CurrentRPM, Power, IsConnectedGrid, idAgr);
    }
    public void Update() {
        if (IsConnectedGrid)
        {
            CurrentRPM = 1.0f;
            CurrentPower = CommissioningHydroUnit * MaxPower;
        }
        else 
        {
            CurrentRPM = CommissioningHydroUnit * kRPM;
            CurrentPower = 0.0f;
        }

        UpdateRPM(CurrentRPM, idAgr);
        UpdatePower(CurrentPower, MaxPower, idAgr);
    }
}
