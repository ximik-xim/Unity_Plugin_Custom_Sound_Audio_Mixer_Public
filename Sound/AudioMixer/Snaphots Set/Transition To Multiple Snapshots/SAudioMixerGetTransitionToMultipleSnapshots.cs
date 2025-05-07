using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нужен для возможности перейти к заранее созданным снапшотам(заранее сохраненным настройкам, пр как префаб) в Audio Mixer по ключу 
/// В этой реализации можно смешать несколько снапшотов( на основе весов) для 1 ключа
/// </summary>
public class SAudioMixerGetTransitionToMultipleSnapshots : MonoBehaviour
{
    [SerializeField]
    private List<AbsKeyData<GetDataSODataSnaphotsSetAudioMixer, STransitionToMultipleSnapshotsAudioMixerData>> _list = new List<AbsKeyData<GetDataSODataSnaphotsSetAudioMixer, STransitionToMultipleSnapshotsAudioMixerData>>(); 

    private Dictionary<string, STransitionToMultipleSnapshotsAudioMixerData> _dictionary = new Dictionary<string, STransitionToMultipleSnapshotsAudioMixerData>();

        
    private bool _init = false;
    public bool Init => _init;
    public event Action OnInit;
    
    /// <summary>
    /// Тут обязательно нужно именно метод Start, т.к SetFloat у Audio Mixer работает только после метода Start(если попытаться в Awake, то значение тупо не установиться)  
    /// </summary>
    private void Start()
    {
        foreach (var VARIABLE in _list)
        {
            _dictionary.Add(VARIABLE.Key.GetData().GetKey(),VARIABLE.Data);
            VARIABLE.Data.LocalAwake();
        }
        
        _init = true;
        OnInit?.Invoke();
    }
    
    public STransitionToMultipleSnapshotsAudioMixerData GetVariableLogic(SKeySnaphotsSetAudioMixer key)
    {
        return _dictionary[key.GetKey()];
    }

    public bool IsAddVariableLogic(SKeySnaphotsSetAudioMixer key)
    {
        return _dictionary.ContainsKey(key.GetKey());
    }

    public void AddVariableLogic(SKeySnaphotsSetAudioMixer key, STransitionToMultipleSnapshotsAudioMixerData data)
    {
        if (IsAddVariableLogic(key) == false)
        {
            _dictionary.Add(key.GetKey(), data);
            data.LocalAwake();
        }
    }

    public void RemoveVariableLogic(SKeySnaphotsSetAudioMixer key)
    {
        if (IsAddVariableLogic(key) == true)
        {
            _dictionary.Remove(key.GetKey());
        }
    }
}
