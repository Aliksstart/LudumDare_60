using System;

[Serializable]
public class TaskGame
{
    //[SerializeField]
    //public bool isHidenTask { get; }
    public float TargetGeneration;
    public float Concession = 0.05f;
    public float StartInternalTime;
    public float EndInternalTime;
    public RemarkDialog[] startDialog;
    //[SerializeField]
    //private RemarkDialog stopDialog;

}
