using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private bool gameActive;
    [SerializeField]
    private GameOverUIController UIGameOver;
    [SerializeField]
    private UIPlaineHandler UIGameWin;
   
    private Reservoirs reservoirs;
    [SerializeField]
    private int generation = 2;
    [SerializeField]
    UIValueFloatConnected watherLevel;
    [Header("Generations")]
    [SerializeField]
    UIValueFloatConnected[] GenerationCommissioningHydroUnit;
    [SerializeField]
    UIValueFloatConnected[] GenerationRPM;
    [SerializeField]
    UIValueFloatConnected[] GenerationPower;
    [SerializeField]
    UIStateNetButton[] StateConnectedButton;
    [SerializeField]
    private float[] openGenerations;
    [SerializeField]
    private UIGenerationValue UITotalGeneration;
    [SerializeField]
    private UIValueFloatConnected UITargetGeneration;
    [Header("Spillway")]
    [SerializeField]
    UIValueFloatConnected[] UIspillway;
    [SerializeField]
    private float[] openSpillway;

    [Header("Tasks")]
    [SerializeField]
    private TaskGame[] PoolTasks;
    private TaskGame ActiiveTask;
    [SerializeField]
    private int EndShift = 8;

    [SerializeField]
    GameTime gameTime;

    GenerationView[] GenerationViews;
    SpillwayView[] spillway = new SpillwayView[3];

    private void OnGameOver(string cause) {
        gameActive = false;
        gameTime.StopTime();
        UIGameOver.OnActivate(cause);
    }
    private void OnGameVicroy()
    {
        gameActive = false;
        gameTime.StopTime();
        UIGameWin.ForceActivate();
    }

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
            spillway[i] = (openSpillway != null && i < openSpillway.Length) ?
                new SpillwayView(i, UIspillway[i].maxValue, openSpillway[i]) : new SpillwayView(i, UIspillway[i].maxValue);
            UIspillway[i].maxValue = 0;
            spillway[i].UpdateCommissioningHydroUnit += SpillwayUpdate;
            spillway[i].ForceUpdateValue();
        }
        GenerationViews = new GenerationView[generation];
        for (int i = 0; i < generation; i++) {
            GenerationViews[i] = (openGenerations != null && i < openGenerations.Length) ?
                new GenerationView(i, openGenerations[i]) : new GenerationView(i);
            GenerationViews[i].UpdateCommissioningHydroUnit += UpdateGenerationCommissioningHydroUnit;
            GenerationViews[i].UpdateRPM += UpdateRPMHydroUnit;
            GenerationViews[i].UpdatePower += UpdatePowerHydroUnit;
            GenerationViews[i].UpdateConnetedGrid += UpdateConnectToGridHydroUnit;
            GenerationViews[i].ForceUpdateValue();
        }
        watherLevel.maxValue = 1800;
        watherLevel.value = 1400;
        ActiiveTask = new()
        {
            Concession = 0.05f,
            TargetGeneration = 160,
            StartInternalTime = 6,
            EndInternalTime = 7
        };
        gameActive = true;
        
    }
    private void FixedUpdate()
    {
        if (gameActive)
        {
            float hydraulicUnitFlowRate = 0;
            float spillwayUnitFlowRate = 0;
            float dt = Time.fixedDeltaTime;
            float summarryPower = 0;
            for (int i = 0; i < generation; i++)
            {
                hydraulicUnitFlowRate += GenerationViews[i].Update();
                summarryPower += GenerationViews[i].CurrentPower;
            }
            for (int i = 0; i < 3; i++)
            {
                spillwayUnitFlowRate += spillway[i].Update();
            }
            reservoirs.Update(hydraulicUnitFlowRate, spillwayUnitFlowRate, dt);
            if (reservoirs.IsOverflow) OnGameOver("The dam was washed away.");

            if (reservoirs.IsUnderfilling) OnGameOver("The headwater has been drained.");

            TaskGame newTask = null;
            int h = gameTime.Hours;

            for (int i = 0; i < PoolTasks.Length; i++) {
                if (h >= PoolTasks[i].StartInternalTime  && h <= PoolTasks[i].EndInternalTime && ActiiveTask != PoolTasks[i]) {
                    newTask = PoolTasks[i];
                    break;
                }
            }
            if (ActiiveTask == null)
            {
                ActiiveTask = newTask;
                UITargetGeneration.value = ActiiveTask.TargetGeneration;
            }
            else
            {
                if (h >= ActiiveTask.EndInternalTime)
                {
                    float k = ActiiveTask.TargetGeneration * ActiiveTask.Concession;
                    if (summarryPower < ActiiveTask.TargetGeneration - k || summarryPower > ActiiveTask.TargetGeneration + k)
                    {
                        OnGameOver("You ignored the dispatcher's instructions.");
                    }
                    if (newTask != null)
                        ActiiveTask = newTask;
                }
                UITotalGeneration.valueUpdate(summarryPower, ActiiveTask.TargetGeneration, ActiiveTask.Concession);
                UITargetGeneration.value = ActiiveTask.TargetGeneration;
            }
            if (h >= EndShift)
            {
                OnGameVicroy();
            }
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
    public void RestartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
