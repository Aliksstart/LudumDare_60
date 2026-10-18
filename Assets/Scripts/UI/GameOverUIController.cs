using TMPro;
using UnityEngine;

public class GameOverUIController : UIPlaineHandler
{
    TMP_Text text_gameOver;
    private void Awake()
    {
        text_gameOver = GetComponentInChildren<TMP_Text>();
    }
    public void OnActivate(string cause) {
        base.OnActivate();
        text_gameOver.text = $"You lose! {cause}";
    }
}
