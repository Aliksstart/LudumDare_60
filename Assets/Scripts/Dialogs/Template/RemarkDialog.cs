using System;

[Serializable]
public enum TypeD {
    On_duty,
    Boss,
    Subordinate
}

[Serializable]
public class RemarkDialog
{
    public TypeD Speaking;
    public string remark;
}
