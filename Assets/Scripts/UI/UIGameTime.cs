using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameTime : MonoBehaviour
{
    TMP_Text text;
    [SerializeField]
    GameTime gameTime;


    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI[] objs = GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (var obj in objs)
        {
            if (obj.name.Contains("Value"))
                text = obj;
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = gameTime.GetTime();
    }
}
