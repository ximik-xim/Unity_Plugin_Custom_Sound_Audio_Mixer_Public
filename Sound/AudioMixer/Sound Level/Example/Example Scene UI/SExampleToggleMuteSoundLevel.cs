using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SExampleToggleMuteSoundLevel : ExampleAbsSoundStatusMute
{
    public override bool IsInit => true;
    public override event Action OnInit;
    
    public override event Action OnUpdateStatusMute;
    
    [SerializeField] 
    private Toggle _mute;
    
    public override STypeStatusMute GetStatusMute()
    {
        if (_mute.isOn == true)
        {
            return STypeStatusMute.Mute;
        }

        return STypeStatusMute.NoMute;
    }

    private void Awake()
    {
        _mute.onValueChanged.AddListener(ClickMute);
    }

    private void ClickMute(bool isMute)
    {
        OnUpdateStatusMute?.Invoke();
    }

    private void OnDestroy()
    {
        _mute.onValueChanged.RemoveListener(ClickMute);
    }
}
