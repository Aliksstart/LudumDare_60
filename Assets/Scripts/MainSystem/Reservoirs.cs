using System;

public class Reservoirs
{

    public float MaxHeadwater { get; } = 1800;
    public float MinHeadwater { get; } = 800;
    private float CurrentHeadwater = 1400;

    public float MaxTailwater { get; } = 750;
    private float CurrentTailwater = 300;
    public float H { get { return CurrentHeadwater - CurrentTailwater; } }

    private float InFlow = 350;
    private float OutFlow = 150;

    public Action<float, float> Updated;
    public void Update(float hydraulicUnitFlowRate, float dischargeFlowRate, float dt)
    {
        float totalOut = hydraulicUnitFlowRate + dischargeFlowRate;
        CurrentHeadwater += (InFlow - totalOut) * dt;
        CurrentTailwater += (totalOut - OutFlow) * dt;
        Updated(CurrentHeadwater, CurrentTailwater);
    }
}
