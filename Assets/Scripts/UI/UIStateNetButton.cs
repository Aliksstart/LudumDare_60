using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStateNetButton : MonoBehaviour
{
    [SerializeField]
    private Color GridOFF_backroud;
    [SerializeField]
    private Color GridON_background;
    [SerializeField]
    private Color GridOFF_foreground;
    [SerializeField]
    private Color GridON_foreground;

    [SerializeField]
    private Image IndGridOn;
    [SerializeField]
    private Image IndGridOff;

    private bool _state;
    public void UpdateStete(bool state)
    {
        _state = !state;
        ColorUpdate();
    }
    // Start is called before the first frame update
    void Awake()
    {
        IndGridOff.color = GridOFF_backroud;
        IndGridOn.color = GridON_background;
    }
    private void ColorUpdate()
    {
        if (!_state)
        {
            IndGridOff.color = GridOFF_backroud;
            IndGridOn.color = GridON_foreground;
        }
        else
        {
            IndGridOff.color = GridOFF_foreground;
            IndGridOn.color = GridON_background;
        }
    }
    void Update()
    {
        ColorUpdate();
    }
}
