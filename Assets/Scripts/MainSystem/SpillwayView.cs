using System;

public class SpillwayView : HydraulicUnit
{
    public SpillwayView(int id, float max = 750) : base(id)
    {
        maxUnitFlowRate = max;
    }
    public new float Update()
    {
        return base.Update();
    }
}
