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
}
