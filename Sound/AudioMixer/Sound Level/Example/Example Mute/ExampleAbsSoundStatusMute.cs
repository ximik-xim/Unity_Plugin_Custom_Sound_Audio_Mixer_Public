using System;
using UnityEngine;

public abstract class ExampleAbsSoundStatusMute : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit; 
    
    public abstract event Action OnUpdateStatusMute;
    public abstract STypeStatusMute GetStatusMute();
}

public enum STypeStatusMute
{
    Mute,
    NoMute
}
