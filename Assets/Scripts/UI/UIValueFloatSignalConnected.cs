using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIValueFloatSignalConnected : UIValueFloatConnected
{
    [SerializeField] private float blinkDuration = 4f;
    [SerializeField] private float blinkInterval = 0.25f;
    [SerializeField]
    private Color signalColor = new Color32(159, 137, 55, 255);

    private Color normalColor;
    private Coroutine blinkCoroutine;

    private float CurrVal;

    public override void Start()
    {
        base.Start();
        normalColor = TextValue.color;
        CurrVal = float.MinValue;
    }

    protected override string Formatter() => base.Formatter();
    public override void valueUpdate(float val)
    {
        if (val == CurrVal)
            return;
        CurrVal = val;
        if (blinkCoroutine != null)
            StopCoroutine(blinkCoroutine);
        blinkCoroutine = StartCoroutine(Blink());
        base.valueUpdate(val);
    }
    IEnumerator Blink()
    {
        float endTime = Time.unscaledTime + blinkDuration;
        bool highlighted = true;

        while (Time.unscaledTime < endTime)
        {
            TextValue.color = highlighted ? signalColor : normalColor;
            highlighted = !highlighted;

            yield return new WaitForSecondsRealtime(
                Mathf.Max(0.01f, blinkInterval));
        }

        TextValue.color = normalColor;
        blinkCoroutine = null;
    }
    private void OnDisable()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            TextValue.color = normalColor;
            blinkCoroutine = null;
        }
    }
}
