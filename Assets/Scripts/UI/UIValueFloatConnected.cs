using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIValueFloatConnected : UIValueConnected
{
    public new float value;
    public new float maxValue;
    protected override string Formatter()
    {
        if (maxValue == 0.0f)
        {
            return base.unitOfMeasurement == "%" ? (value * 100).ToString() : value.ToString();
        }
        else
        { 
            return $"{value}/{maxValue}"; 
        }

    }

    protected override void valueUpdate(float val)
    {
        value = val;
    }
}
