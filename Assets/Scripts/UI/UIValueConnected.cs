using TMPro;
using UnityEngine;

public abstract class UIValueConnected : MonoBehaviour
{
    [SerializeField]
    private string nameP;
    protected TMP_Text text;
    protected object value;
    protected object maxValue;
    [SerializeField]
    protected string unitOfMeasurement;
    protected abstract string Formatter();
    protected abstract void valueUpdate(float val);
    public void Start()
    {
        text = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        text.text = $"{nameP}: {Formatter()} {unitOfMeasurement}";
    }
}
