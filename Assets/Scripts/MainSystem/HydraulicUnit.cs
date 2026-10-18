using System;

public class HydraulicUnit
{
    protected int idAgr { get; private set; }
    private float stepButton = 0.05f;
    protected float maxUnitFlowRate;
    
    public float CommissioningHydroUnit { get; private set; }

    public Action<float, int> UpdateCommissioningHydroUnit;
    public void Commissionin()
    {
        float x = CommissioningHydroUnit + stepButton;
        CommissioningHydroUnit = x > 1.0f ? 1.0f : x;
        UpdateCommissioningHydroUnit(CommissioningHydroUnit, idAgr);
    }
    public void Shutdown()
    {
        float x = CommissioningHydroUnit - stepButton;
        CommissioningHydroUnit = x < 0.0f ? 0.0f : x;
        UpdateCommissioningHydroUnit(CommissioningHydroUnit, idAgr);
    }
    public HydraulicUnit(int id)
    {
        idAgr = id;
    }
    protected float Update() {
        return CommissioningHydroUnit * maxUnitFlowRate;
    }
}