using TListPlugin; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyIndifNameSODataAudioMixerGetAndSetVariable : AbsIdentifierAndData<AudioMixerGetAndSetVariableIndifNameSO, string, SKeyAudioMixerGetAndSetVariable>
{

    [SerializeField]
    private SKeyAudioMixerGetAndSetVariable _dataKey;


    public override SKeyAudioMixerGetAndSetVariable GetKey()
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
        _dataKey = JsonUtility.FromJson<SKeyAudioMixerGetAndSetVariable>(json);
    }
#endif
}
