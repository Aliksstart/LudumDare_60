using UnityEngine;

public class Game : MonoBehaviour
{
    private Reservoirs reservoirs;
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
    [SerializeField]
    UIValueFloatConnected[] UIspillway;

    GenerationView[] GenerationViews;
    SpillwayView[] spillway = new SpillwayView[3];

    private void ReservoirsUpdate(float Headwater, float Tailwater)
    {
        watherLevel.value = Headwater;
        watherLevel.maxValue = reservoirs.MaxHeadwater;
    }

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

    private void SpillwayUpdate(float val, int id)
    {
        UIspillway[id].value = val;
    }

    void Start()
    {
        reservoirs = new Reservoirs();
        reservoirs.Updated += ReservoirsUpdate;
        for (int i = 0; i < 3; i++)
        {
            spillway[i] = new SpillwayView(i, UIspillway[i].maxValue);
            UIspillway[i].maxValue = 0;
            spillway[i].UpdateCommissioningHydroUnit += SpillwayUpdate;
        }
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
    private void FixedUpdate()
    {
        float hydraulicUnitFlowRate = 0;
        float spillwayUnitFlowRate = 0;
        float dt = Time.fixedDeltaTime;
        for (int i = 0; i < generation; i++)
        {
            hydraulicUnitFlowRate += GenerationViews[i].Update();
        }
        for (int i = 0; i < 3; i++)
        {
            spillwayUnitFlowRate += spillway[i].Update();
        }
        reservoirs.Update(hydraulicUnitFlowRate, spillwayUnitFlowRate, dt);
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
    public void OpenSpillway(int sw)
    {
        if (sw >= spillway.Length)
            throw new UnityException();
        spillway[sw].Commissionin();
    }
    public void ClosedSpillway(int sw)
    {
        if (sw >= spillway.Length)
            throw new UnityException();
        spillway[sw].Shutdown();
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
