using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Нужен для получения значения переменной из Audio Mixer по ключу
/// 1 - указываю по какому ключу буду получать значение переменной
/// --------------------ДАННЫЕ---------------------------
/// 2 - указываю микшер из которого буду получ. значение
/// 3 - указываю ключ по которому в этом микшере будет находиться переменная(в ручную нужно создать ключ в Audio Mixer и внести этот же ключ в список TList)
/// (еще один ключ (3) нужен отдельно, что бы можно было к примеру сделать у всех микшеров одинаковый ключ для получ. каких то значений, но при этом ключ для получения
/// значений переменной (1) у них будет разный)
/// </summary>
public class SAudioMixerGetAndSetVariable : MonoBehaviour
{
    [SerializeField]
    private List<AbsKeyData<GetDataSODataAudioMixerGetAndSetVariable, SAudioMixerGetAndSetVariableData>> _list = new List<AbsKeyData<GetDataSODataAudioMixerGetAndSetVariable, SAudioMixerGetAndSetVariableData>>(); 

    private Dictionary<string, SAudioMixerGetAndSetVariableData> _dictionary = new Dictionary<string, SAudioMixerGetAndSetVariableData>();

        
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
        }
        
        _init = true;
        OnInit?.Invoke();
    }
    
    public SAudioMixerGetAndSetVariableData GetVariableLogic(SKeyAudioMixerGetAndSetVariable key)
    {
        return _dictionary[key.GetKey()];
    }

    public bool IsAddVariableLogic(SKeyAudioMixerGetAndSetVariable key)
    {
        return _dictionary.ContainsKey(key.GetKey());
    }

    public void AddVariableLogic(SKeyAudioMixerGetAndSetVariable key, SAudioMixerGetAndSetVariableData data)
    {
        if (IsAddVariableLogic(key) == false)
        {
            _dictionary.Add(key.GetKey(), data);
        }
    }



    public void RemoveVariableLogic(SKeyAudioMixerGetAndSetVariable key)
    {
        if (IsAddVariableLogic(key) == true)
        {
            _dictionary.Remove(key.GetKey());
        }
    }
}



