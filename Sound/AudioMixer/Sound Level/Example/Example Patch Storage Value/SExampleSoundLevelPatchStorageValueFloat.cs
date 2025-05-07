using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Пример хранилище через логику сохранения данных, для сохранения уровня звука
/// </summary>
public class SExampleSoundLevelPatchStorageValueFloat : SSoundLevelAbsPatchStorageValue
{
    [SerializeField]
    private GetDKOPatch _patchStorage;
    private SD_SwitherStorageFloat _getGeneralLogic;
    
    [SerializeField] 
    private GetDataSO_SD_KeyStorageFloatVariable _key;

    private SD_AbsFloatStorage _currentStorage;

    public override event Action OnUpdateData;
    
    public override StatusStorageAction LastStatusSaveData
    {
        get => _lastStatusSaveData;
    }

    private StatusStorageAction _lastStatusSaveData;



    public override event Action OnSaveData;

    
    public override event Action OnInit;
    private bool _init = false;
    public override bool IsInit()
    {
        return _init;
    }
    
    
    private void Awake()
    {
        if (_patchStorage.Init == false)
        {
            _patchStorage.OnInit += OnInitFind;
            return;
        }

        InitFind();
    }
    
    public override void SaveData(TaskInfo taskInfo, bool urgentSaving = false)
    {
        _currentStorage.SaveData(taskInfo, urgentSaving);
    }

    public override void SetValue(float value)
    {
        _currentStorage.SetData(_key.GetData(), value);
    }

    public override float GetValue()
    {
        return _currentStorage.GetData(_key.GetData());
    }
    
   

    private void OnInitFind()
    {
        _patchStorage.OnInit -= OnInitFind;
        InitFind();
    }
    

    private void InitFind()
    {
        _getGeneralLogic = ((DKODataInfoT<SD_SwitherStorageFloat>)_patchStorage.GetDKO()).Data;
        _getGeneralLogic.OnUpdateStorageLocal += OnUpdateStorageLocal;

        _currentStorage = _getGeneralLogic.GetCurrentStorage();
        
        _currentStorage.OnUpdateValue += OnUpdateValueStorage;
        _currentStorage.OnUpdateData += OnUpdateDataStorage;
        _currentStorage.OnSaveDataComplited += OnSave;
        
        _init = true;
        OnInit?.Invoke();   
    }

    private void OnUpdateStorageLocal()
    {
        _currentStorage.OnUpdateValue -= OnUpdateValueStorage;
        _currentStorage.OnUpdateData -= OnUpdateDataStorage;
        _currentStorage.OnSaveDataComplited -= OnSave;
        
        _currentStorage = _getGeneralLogic.GetCurrentStorage();
        _currentStorage.OnUpdateValue += OnUpdateValueStorage;
        _currentStorage.OnUpdateData += OnUpdateDataStorage;
        _currentStorage.OnSaveDataComplited += OnSave;
        

        OnUpdateData?.Invoke();    
    }

    private void OnUpdateDataStorage()
    {
        if (_currentStorage.LastStatusUpdateData == StatusStorageAction.Ok)
        {
            OnUpdateData?.Invoke();
        }
    }

    private void OnUpdateValueStorage(SD_KeyStorageFloatVariable obj)
    {
        if (obj == _key.GetData()) 
        {
            OnUpdateData?.Invoke();
        }
    }

    private void OnDestroy()
    {
        _getGeneralLogic.OnUpdateStorageLocal -= OnUpdateStorageLocal;

        if (_currentStorage != null) 
        {
            _currentStorage.OnUpdateValue -= OnUpdateValueStorage;
            _currentStorage.OnUpdateData -= OnUpdateDataStorage;
        }
    }

    private void OnSave()
    {
        OnSaveData?.Invoke();
        _lastStatusSaveData = _currentStorage.LastStatusSaveData;
    }
}
