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
}