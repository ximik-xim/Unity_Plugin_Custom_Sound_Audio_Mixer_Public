using TListPlugin; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyIndifNameSODataSoundLevelAudioMixer : AbsIdentifierAndData<SoundLevelAudioMixerIndifNameSO, string, SKeySoundLevelAudioMixer>
{
    [SerializeField]
    private SKeySoundLevelAudioMixer _dataKey;


    public override SKeySoundLevelAudioMixer GetKey()
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
        _dataKey = JsonUtility.FromJson<SKeySoundLevelAudioMixer>(json);
    }
#endif
}
