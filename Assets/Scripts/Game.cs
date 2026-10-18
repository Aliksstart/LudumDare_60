using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private int generation = 2;
    [SerializeField]
    UIValueFloatConnected watherLevel;
    [SerializeField]
    UIValueFloatConnected[] GenerationCommissioningHydroUnit;
    [SerializeField]
    UIValueFloatConnected[] GenerationRPM;
    [SerializeField]
    UIValueFloatConnected[] GenerationPower;
    [SerializeField]
    UIStateNetButton[] StateConnectedButton;

    GenerationView[] GenerationViews;

    private void UpdateGenerationCommissioningHydroUnit(float val, int id)
    {
        GenerationCommissioningHydroUnit[id].value = val;
    }
    private void UpdateRPMHydroUnit(float val, int id)
    {
        GenerationRPM[id].value = val;
    }
    private void UpdatePowerHydroUnit(float val, float max, int id)
    {
        GenerationPower[id].value = val;
        GenerationPower[id].maxValue = max;
    }

    private void UpdateConnectToGridHydroUnit(float rpm, float power, bool isConnected, int id)
    {
        StateConnectedButton[id].UpdateStete(isConnected);
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerationViews = new GenerationView[generation];
        for (int i = 0; i < generation; i++) { 
            GenerationViews[i] = new GenerationView(i);
            GenerationViews[i].UpdateCommissioningHydroUnit += UpdateGenerationCommissioningHydroUnit;
            GenerationViews[i].UpdateRPM += UpdateRPMHydroUnit;
            GenerationViews[i].UpdatePower += UpdatePowerHydroUnit;
            GenerationViews[i].UpdateConnetedGrid += UpdateConnectToGridHydroUnit;
        }
        watherLevel.maxValue = 1800;
        watherLevel.value = 1400;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < generation; i++)
        {
            GenerationViews[i].Update();
        }
    }
    public void OpenGen(int gen)
    {
        if (gen >= generation)
            throw new UnityException();
        GenerationViews[gen].Commissionin();
    }
    public void ClosedGen(int gen)
    {
        if (gen >= generation)
            throw new UnityException();
        GenerationViews[gen].Shutdown();
    }
    public void ConnectGeneratorButton(int gen)
    {
        if (gen >= generation)
            throw new UnityException();
        if (GenerationViews[gen].IsConnectedGrid)
            GenerationViews[gen].DisconnectionGen();
        else
            GenerationViews[gen].ConnectionGen();
    }
}
