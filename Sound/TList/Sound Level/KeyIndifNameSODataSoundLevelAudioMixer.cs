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
}
