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
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"Time: {gameTime.GetTime()}";
    }
}
