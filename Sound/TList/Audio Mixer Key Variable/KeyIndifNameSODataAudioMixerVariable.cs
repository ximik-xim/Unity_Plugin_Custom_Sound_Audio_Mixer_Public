using TListPlugin; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyIndifNameSODataAudioMixerVariable : AbsIdentifierAndData<AudioMixerVariableIndifNameSO, string, SKeyAudioMixerVarible>
{

    [SerializeField]
    private SKeyAudioMixerVarible _dataKey;


    public override SKeyAudioMixerVarible GetKey()
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
        _dataKey = JsonUtility.FromJson<SKeyAudioMixerVarible>(json);
    }
#endif
}