using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Пример хранилище на превсах, для сохранения уровня звука
/// </summary>
public class SExampleSoundLevelPatchStoragePrefs : SSoundLevelAbsPatchStorageValue
{
    [SerializeField] 
    private string _prefsPath;

    public override bool IsInit()
    {
        return true;
    }

    public override event Action OnInit;
    
    public override event Action OnUpdateData;

    private void Awake()
    {
        OnInit?.Invoke();
        OnUpdateData?.Invoke();
    }

    public override void SetValue(float value)
    {
        PlayerPrefs.SetFloat(_prefsPath, value);
        OnUpdateData?.Invoke();
    }

    public override float GetValue()
    {
       return PlayerPrefs.GetFloat(_prefsPath);
    }

    public override event Action OnSaveData;
    public override void SaveData(TaskInfo taskInfo, bool urgentSaving = false)
    {
        PlayerPrefs.Save();
        OnSaveData?.Invoke();
    }

    public override StatusStorageAction LastStatusSaveData { get => StatusStorageAction.Ok; }
}
