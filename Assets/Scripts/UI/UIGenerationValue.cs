using UnityEngine;

public class UIGenerationValue : UIValueFloatConnected
{
    [SerializeField]
    Color Ok;
    [SerializeField]
    Color Warn;
    [SerializeField]
    Color Err;
    protected override string Formatter() => base.Formatter();

    public void valueUpdate(float val, float targer, float concession)
    {
        float k = targer * concession;
        if (val == targer)
            TextValue.color = Ok;
        else if (val < targer - k || val > targer + k)
            TextValue.color = Err;
        else
            TextValue.color = Warn;
        
        base.valueUpdate(val);
    }
}
