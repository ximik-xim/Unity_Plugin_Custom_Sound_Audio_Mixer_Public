using System;
using UnityEngine;

/// <summary>
/// Пример хранилище на превсах, для сохранения уровня звука(через хранилеще float)
/// </summary>
public class SExampleSoundLevelPatchSDStorageFloatPrefs : SSoundLevelAbsPatchStorageValue
{
    [SerializeField] 
    private SD_StorageDataFloatPrefs _storageSaveData;
    [SerializeField] 
    private GetDataSO_SD_KeyStorageFloatVariable _key;

    [SerializeField]
    private float _returnValueNotFount;

    public override bool IsInit()
    {
        return _storageSaveData.IsInit;
    }

    public override event Action OnInit
    {
        add
        {
            _storageSaveData.OnInit += value;
        }

        remove
        {
            _storageSaveData.OnInit -= value;
        }
    }

    public override event Action OnUpdateData;


    private void Awake()
    {
        _storageSaveData.OnUpdateValue += OnUpdateValueStorage;
        _storageSaveData.OnUpdateData += OnUpdateDataStorage;
    }

    private void OnUpdateDataStorage()
    {
        OnUpdateData?.Invoke();
    }

    private void OnUpdateValueStorage(SD_KeyStorageFloatVariable obj)
    {
        if (obj == _key.GetData()) 
        {
            OnUpdateData?.Invoke();
        }
    }

    public override void SetValue(float value)
    {
        _storageSaveData.SetData(_key.GetData(), value);
    }

    public override float GetValue()
    {
        if (_storageSaveData.IsThereData(_key.GetData()) == false)
        {
            return _returnValueNotFount;
        }
        
        return _storageSaveData.GetData(_key.GetData());
    }

    public override event Action OnSaveData
    {
        add
        {
            _storageSaveData.OnSaveDataComplited += value;
        }

        remove
        {
            _storageSaveData.OnSaveDataComplited -= value;
        }
    }
    public override void SaveData(TaskInfo taskInfo, bool urgentSaving = false)
    {
        _storageSaveData.SaveData(taskInfo, urgentSaving);
    }

    public override StatusStorageAction LastStatusSaveData => _storageSaveData.LastStatusSaveData;

    private void OnDestroy()
    {
        _storageSaveData.OnUpdateValue -= OnUpdateValueStorage;
        _storageSaveData.OnUpdateData -= OnUpdateDataStorage;
    }
}
