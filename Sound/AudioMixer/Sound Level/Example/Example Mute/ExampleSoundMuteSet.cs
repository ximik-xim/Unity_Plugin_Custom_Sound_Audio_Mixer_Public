using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Пример
/// Устанавливает мут указанным контроллерам уровня звука по ключу 
/// </summary>
public class ExampleSoundMuteSet : MonoBehaviour
{
    [SerializeField]
    private ExampleAbsSoundStatusMute _statusMute;
    
    [SerializeField] 
    private SAudioMixerGetSoundLevelMono _audioMixerSoundLeve;

    [SerializeField] 
    private List<GetDataSODataSoundLevelAudioMixer> _keySoundValue;
    
    [SerializeField] 
    private GetDataSODataTaskMuteSoundLevel _taskMute;
    private STaskDataMuteSoundLevel _taskDataMute;
    [SerializeField] 
    private string _textMute;
    
    private void Awake()
    {
        if (_audioMixerSoundLeve.Init == false)
        {
            _audioMixerSoundLeve.OnInit += OnInitMixer;
        }
        
        if (_statusMute.IsInit == false)
        {
            _statusMute.OnInit += OnInitStatusMute;
        }

        CheckInit();
    }

    private void OnInitMixer()
    {
        _audioMixerSoundLeve.OnInit -= OnInitMixer;
        CheckInit();
    }
    
    private void OnInitStatusMute()
    {
        _statusMute.OnInit -= OnInitStatusMute;
        CheckInit();
    }


    private void CheckInit()
    {
        if (_audioMixerSoundLeve.Init == true && _statusMute.IsInit == true) 
        {
            Init();
        }
    }
    
    private void Init()
    {
        _taskDataMute = new STaskDataMuteSoundLevel(_textMute);
        _statusMute.OnUpdateStatusMute += OnUpdateStatusMute;
        OnUpdateStatusMute();
    }

    private void OnUpdateStatusMute()
    {
        if (_statusMute.GetStatusMute() == STypeStatusMute.Mute)
        {
            SetKey();
        }
        
        if (_statusMute.GetStatusMute() == STypeStatusMute.NoMute)
        {
            RemoveKey();
        }
    }
    
    private void SetKey()
    {
        foreach (var VARIABLE in _keySoundValue)
        {
            var soundLeve = _audioMixerSoundLeve.GetSoundLevelData(VARIABLE.GetData());
            if (soundLeve.IsKeyMute(_taskMute.GetData()) == false)
            {
                soundLeve.SetKeyMute(_taskMute.GetData(), _taskDataMute);
            }
        }
    }

    private void RemoveKey()
    {
        foreach (var VARIABLE in _keySoundValue)
        {
            var soundLeve = _audioMixerSoundLeve.GetSoundLevelData(VARIABLE.GetData());
            if (soundLeve.IsKeyMute(_taskMute.GetData()) == true)
            {
                soundLeve.RemoveKeyMute(_taskMute.GetData());
            }
        }
    }
    
    private void OnDestroy()
    {
        _statusMute.OnUpdateStatusMute -= OnUpdateStatusMute;

        RemoveKey();
    }
}
