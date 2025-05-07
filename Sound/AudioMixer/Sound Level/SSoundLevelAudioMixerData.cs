using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Обертка над SAudioMixerGetAndSetVariableData предназначеная для работы с уровнем громкости
///
/// Может
/// - Мутить звук если добавлина Task на мут(сохраняя перед этим уровень звука который был)
/// Если изменить уровень звука при муте, то устоновленный уровень звука сохраниться и будет применен когда будут удлаены все Task на мут
/// - Имеет абстрактную логику сохранения уровня звука(тупо сохраняет float переменную)
/// </summary>
[System.Serializable]
public class SSoundLevelAudioMixerData
{
    [SerializeField] 
    private SAudioMixerGetAndSetVariableData _variableData;
    
    //Тут абстракция, куда буду сохранять данные об уровне звука(может через хранилеще, а может в тупую через привсы пока сделаю)
    [SerializeField] 
    private SSoundLevelAbsPatchStorageValue _patchStorageValue;
        
    public event Action OnUpdateStatusMute;
    private bool _isMute = false;
    public bool IsMute => _isMute;
   
    public event Action OnUpdateCountTaskMute;
    private Dictionary<string, STaskDataMuteSoundLevel> _taskDataMute = new Dictionary<string, STaskDataMuteSoundLevel>();
    
    
    private float _muteValue = -80f;

    public event Action OnUpdateValue;

    public event Action OnInit;
    private bool _init = false;
    public bool IsInit => _init;
    public void SetKeyMute(SKeyTaskDataMuteSoundLevel key,STaskDataMuteSoundLevel task)
    {
        _taskDataMute.Add(key.GetKey(),task);
        OnUpdateCountTaskMute?.Invoke();
        
        if (_isMute == false)
        {
            _isMute = true;
            SetMute();
            OnUpdateStatusMute?.Invoke();
        }
    }
    public void RemoveKeyMute(SKeyTaskDataMuteSoundLevel key)
    {
        _taskDataMute.Remove(key.GetKey());
        OnUpdateCountTaskMute?.Invoke();
        
        if (_taskDataMute.Count == 0) 
        {
            if (_isMute == true)
            {
                _isMute = false;
                RemoveMute();
                OnUpdateStatusMute?.Invoke();
            }
        }
    }
    
    public bool IsKeyMute(SKeyTaskDataMuteSoundLevel key)
    {
        return _taskDataMute.ContainsKey(key.GetKey());
    }


    private void SetMute()
    {
        float soundLevel = _variableData.GetValue();
        // Вот тут сохраняю текущий уровень громкости и уст знач громкости на 0
        
        _patchStorageValue.SetValue(soundLevel);
        _patchStorageValue.SaveData(new TaskInfo("tt"));
        
        _variableData.SetValue(_muteValue);
        OnUpdateValue?.Invoke();
    }
    
    private void RemoveMute()
    {
        float soundLevel = _patchStorageValue.GetValue();
        _variableData.SetValue(soundLevel);
        
        OnUpdateValue?.Invoke();
    }


    public void SetValue(float soundLevel)
    {
        if (_isMute == false)
        {
            _variableData.SetValue(soundLevel);
            OnUpdateValue?.Invoke();
        }
        
        _patchStorageValue.SetValue(soundLevel);
        _patchStorageValue.SaveData(new TaskInfo("tt"));
    }

    public float GetValue()
    {
        //Возможно, тут в случаае мута, нужно возращать данные из хранилеща(т.к, как только мут пройдет звук вернеться)

        if (_isMute == true) 
        {
            return _patchStorageValue.GetValue();
        }
        
        return _variableData.GetValue();
    }

    private void UpdateDataStorage()
    {
        if (_isMute == false)
        {
            if (_variableData.GetValue() != _patchStorageValue.GetValue()) 
            {
                _variableData.SetValue(_patchStorageValue.GetValue());
                OnUpdateValue?.Invoke();
            }
            
        }
    }

    public void Init()
    {
        if (_init == false)
        {
            if (_patchStorageValue.IsInit() == false) 
            {
                _patchStorageValue.OnInit -= OnInitPatch;
                _patchStorageValue.OnInit += OnInitPatch;
                return;
            }

            InitPatch();
        }
    }

    private void OnInitPatch()
    {
        _patchStorageValue.OnInit -= OnInitPatch;
        InitPatch();
    }
    
    private void InitPatch()
    {
        _patchStorageValue.OnUpdateData += UpdateDataStorage;
        UpdateDataStorage();
        
        _init = true;
        OnInit?.Invoke();
    }

    public void Destory()
    {
        _patchStorageValue.OnUpdateData -= UpdateDataStorage;
    }
}
