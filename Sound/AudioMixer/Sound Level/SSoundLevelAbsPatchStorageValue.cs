using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// абстракция для сохранения данных об уровне звука
/// (усложнено для возможности сохранения данных на сервере, а значит через callback)
/// </summary>
public abstract class SSoundLevelAbsPatchStorageValue : MonoBehaviour
{
    public abstract bool IsInit();
    public abstract event Action OnInit;

    public abstract event Action OnUpdateData;
    
    public abstract void SetValue(float value);
    public abstract float GetValue();
    
    public abstract event Action OnSaveData;
    public abstract void SaveData(TaskInfo taskInfo, bool urgentSaving = false);
    public abstract StatusStorageAction LastStatusSaveData { get; }
}
