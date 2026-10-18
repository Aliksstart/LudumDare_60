using TMPro;
using UnityEngine;

public class UIStateNetButton : MonoBehaviour
{
    private bool _state;
    [SerializeField]
    private TMP_Text text;
    public void UpdateStete(bool state)
    {
        _state = state;
    }
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = _state ? "Grid On" : "Grid off";
    }
}
