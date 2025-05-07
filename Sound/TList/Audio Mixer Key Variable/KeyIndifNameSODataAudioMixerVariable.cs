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
}