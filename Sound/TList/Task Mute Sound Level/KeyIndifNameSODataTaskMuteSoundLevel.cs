using TListPlugin; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyIndifNameSODataTaskMuteSoundLevel : AbsIdentifierAndData<TaskMuteSoundLevelIndifNameSO, string, SKeyTaskDataMuteSoundLevel>
{

    [SerializeField]
    private SKeyTaskDataMuteSoundLevel _dataKey;


    public override SKeyTaskDataMuteSoundLevel GetKey()
    {
        return _dataKey;
    }
    
#if UNITY_EDITOR
    public override string GetJsonSaveData()
    {
        return JsonUtility.ToJson(_dataKey);
    }

    public override void SetJsonData(string json)
    {
        _dataKey = JsonUtility.FromJson<SKeyTaskDataMuteSoundLevel>(json);
    }
#endif
}