using System;

public class GenerationView : HydraulicUnit
{
    // ----
    private float kPower = 0.015f;
    private float kRPM = 3f;
    // ----
    public bool IsConnectedGrid { get; private set; }
    public Action<float, float, bool, int> UpdateConnetedGrid;
    private float MaxPower = 400;
    public float CurrentPower { get; private set; }
    public Action<float, float, int> UpdatePower;
    private float MaxRPM = 1.3f;
    public float CurrentRPM { get; private set; }
    public Action<float, int> UpdateRPM;
    public GenerationView(int id, float cur = 0.0f) : base(id, cur)
    {
        maxUnitFlowRate = 250;
        IsConnectedGrid = cur > 0.0f ? true : false;
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
    public new float Update() {
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
        return base.Update();
    }
}
