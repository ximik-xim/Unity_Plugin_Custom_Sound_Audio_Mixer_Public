using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
/// <summary>
/// Пример мута звука
/// </summary>
public class SExampleGMIsMuteSoundLevel : MonoBehaviour
{
    [SerializeField] 
    private GameObject _IsMute;

    [SerializeField] 
    private GetDataSODataSoundLevelAudioMixer _key;

    [SerializeField] 
    private GetDKOPatch _patchAudioMixerGetSoundLevelMono;
    private SAudioMixerGetSoundLevelMono _mixerGetSoundLevelData;

    private SSoundLevelAudioMixerData _soundLevelData;
    
    
    private void Awake()
    {
        if (_patchAudioMixerGetSoundLevelMono.Init == false) 
        {
            _patchAudioMixerGetSoundLevelMono.OnInit += OnInitPatch;
            return;
        }

        InitPatch();

    }

    private void OnInitPatch()
    {
        _patchAudioMixerGetSoundLevelMono.OnInit -= OnInitPatch;
        InitPatch();
    }

    private void InitPatch()
    {
        var DKOData = (DKODataInfoT<SAudioMixerGetSoundLevelMono>)_patchAudioMixerGetSoundLevelMono.GetDKO();
        _mixerGetSoundLevelData = DKOData.Data;
        
        if (_mixerGetSoundLevelData.Init == false) 
        {
            _mixerGetSoundLevelData.OnInit += OnInit;
            return;
        }

        Init();
    }

    private void OnInit()
    {
        _mixerGetSoundLevelData.OnInit -= OnInit;
        Init();
    }

    private void Init()
    {
        _soundLevelData = _mixerGetSoundLevelData.GetSoundLevelData(_key.GetData());

        _soundLevelData.OnUpdateStatusMute += OnUpdateStatusMute;
        OnUpdateStatusMute();
    }

    private void OnUpdateStatusMute()
    {
        if (_soundLevelData.IsMute == true)
        {
            _IsMute.SetActive(true);
        }
        else
        {
            _IsMute.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        _soundLevelData.OnUpdateStatusMute -= OnUpdateStatusMute;
    }
}
