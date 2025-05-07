using System;
using UnityEngine;

public class ExampleSoundStatusMuteInspector : ExampleAbsSoundStatusMute
{
    public override bool IsInit => true;
    public override event Action OnInit;
    public override event Action OnUpdateStatusMute;
    public override STypeStatusMute GetStatusMute()
    {
        return _statusMute;
    }

    [SerializeField]
    private STypeStatusMute _statusMute;


    private void OnValidate()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            OnUpdateStatusMute?.Invoke();
        }
#endif
    }
}
