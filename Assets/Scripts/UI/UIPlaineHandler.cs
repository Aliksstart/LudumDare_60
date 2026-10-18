using UnityEngine;

public class UIPlaineHandler : MonoBehaviour
{
    public void ForceActivate()
    {
        OnActivate();
    }

    public void ForceDeactivate()
    {
        OnDeactivate();
    }

    void Start()
    {
        OnDeactivate();
    }

    protected void OnActivate()
    {
        gameObject.SetActive(true);
    }

    protected void OnDeactivate()
    {
        gameObject.SetActive(false);
    }
}
