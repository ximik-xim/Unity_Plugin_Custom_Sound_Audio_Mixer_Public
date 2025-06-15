using TListPlugin; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyIndifNameSODataSnaphotsSetAudioMixer : AbsIdentifierAndData<SnaphotsSetAudioMixerIndifNameSO, string, SKeySnaphotsSetAudioMixer>
{

    [SerializeField]
    private SKeySnaphotsSetAudioMixer _dataKey;


    public override SKeySnaphotsSetAudioMixer GetKey()
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
        _dataKey = JsonUtility.FromJson<SKeySnaphotsSetAudioMixer>(json);
    }
#endif
}