using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIValueConnected : MonoBehaviour
{
    [SerializeField]
    private string nameP;
    protected TMP_Text text;
    protected object value;
    protected object maxValue;
    [SerializeField]
    protected string unitOfMeasurement;

    [SerializeField]
    protected TMP_Text TextValue;
    [SerializeField]
    TMP_Text TextMeasurement;

    [SerializeField]
    private Vector2 TextValuePosition = new Vector2(-30f, -60f);
    [SerializeField]
    private Vector2 TextMeasurementPosition = new Vector2(-90f, -36.9f);
    [SerializeField]
    private Vector2 TextMeasurementSize = new Vector2(10f, 50f);
    protected abstract string Formatter();
    protected abstract void valueUpdate(float val);
    public void Start()
    {
        text = GetComponent<TMP_Text>();

        TextMeshProUGUI[] objs = GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (var obj in objs)
        {
            if (obj.name.Contains("Value"))
                TextValue = obj;
            if (obj.name.Contains("M"))
                TextMeasurement = obj;
        }

        // локальное смещение, а не мировое
        if (TextValue != null)
        {
            // создаём фон
            GameObject bg = new GameObject("ValueBG");
            bg.transform.SetParent(TextValue.transform.parent, false);

            Image img = bg.AddComponent<Image>();
            img.color = new Color(0.02f, 0.02f, 0.02f, 1f);
            img.raycastTarget = false;

            RectTransform bgRt = bg.GetComponent<RectTransform>();
            RectTransform textRt = TextValue.rectTransform;

            // фон занимает место текста + отступы
            bgRt.anchorMin = textRt.anchorMin;
            bgRt.anchorMax = textRt.anchorMax;
            bgRt.pivot = textRt.pivot;
            bgRt.anchoredPosition = textRt.anchoredPosition;
            bgRt.sizeDelta = textRt.sizeDelta + new Vector2(12f, 6f);

            // текст становится ребёнком фона
            TextValue.transform.SetParent(bg.transform, false);

            // текст растягиваем на весь фон (или с небольшими отступами)
            textRt.anchorMin = Vector2.zero;
            textRt.anchorMax = Vector2.one;
            textRt.offsetMin = new Vector2(4f, 2f);
            textRt.offsetMax = new Vector2(-4f, -2f);
        }

        if (TextMeasurement != null)
        {
            RectTransform rt = TextMeasurement.rectTransform;
            rt.anchoredPosition = TextMeasurementPosition;
            rt.sizeDelta = TextMeasurementSize;
        }

        text.text = nameP;
        if (TextMeasurement != null)
            TextMeasurement.text = unitOfMeasurement;
    }
    private void Update()
    {
        TextValue.text = Formatter();
    }
}
