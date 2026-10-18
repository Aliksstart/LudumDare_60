using System;

public class SpillwayView : HydraulicUnit
{
    public SpillwayView(int id, float max = 750, float cur = 0.0f) : base(id, cur)
    {
        maxUnitFlowRate = max;
    }
    public new float Update()
    {
        return base.Update();
    }
}
